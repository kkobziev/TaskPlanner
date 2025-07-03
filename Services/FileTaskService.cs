using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskPlanner.Models;

namespace TaskPlanner.Services
{
    public class FileTaskService : ITaskService
    {
        private readonly string _filePath;
        private readonly ILogger<FileTaskService> _logger;

        public FileTaskService(ILogger<FileTaskService> logger, IWebHostEnvironment env, string filePath = "Data/tasks.json")
        {
            _logger = logger;
            // Resolve to absolute path
            _filePath = Path.Combine(env.ContentRootPath, filePath);
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                _logger.LogInformation($"Creating directory: {directory}");
                Directory.CreateDirectory(directory);
            }
            _logger.LogInformation($"Using task storage file: {_filePath}");
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<TaskItem>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            var tasks = await GetAllTasksAsync();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            var tasks = (await GetAllTasksAsync()).ToList();
            tasks.Add(task);
            await SaveTasksAsync(tasks);
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            var tasks = (await GetAllTasksAsync()).ToList();
            var index = tasks.FindIndex(t => t.Id == task.Id);
            if (index != -1)
            {
                tasks[index] = task;
                await SaveTasksAsync(tasks);
            }
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var tasks = (await GetAllTasksAsync()).ToList();
            var taskToRemove = tasks.FirstOrDefault(t => t.Id == id);
            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
                await SaveTasksAsync(tasks);
            }
        }

        public async Task MarkTaskAsCompletedAsync(Guid id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                task.IsCompleted = true;
                await UpdateTaskAsync(task);
            }
        }

        private async Task SaveTasksAsync(List<TaskItem> tasks)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(tasks, options);
                
                // Create a temporary file first, then replace the original
                var tempFile = $"{_filePath}.{Guid.NewGuid()}.tmp";
                await File.WriteAllTextAsync(tempFile, json);
                
                // Replace the original file atomically
                if (File.Exists(_filePath))
                {
                    File.Replace(tempFile, _filePath, null);
                }
                else
                {
                    File.Move(tempFile, _filePath);
                }
                
                _logger.LogInformation($"Successfully saved {tasks.Count} tasks to {_filePath}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving tasks to {_filePath}");
                throw; // Re-throw to be handled by the caller
            }
        }
    }
}