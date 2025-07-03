using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaskPlanner.Models;

namespace TaskPlanner.Services
{
    public class FileTaskService : ITaskService
    {
        private readonly string _filePath;

        public FileTaskService(string filePath = "Data/tasks.json")
        {
            _filePath = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? "Data");
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
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}