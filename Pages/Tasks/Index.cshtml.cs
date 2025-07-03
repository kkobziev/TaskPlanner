using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ITaskService taskService, ILogger<IndexModel> logger)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IList<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string StatusMessageType { get; set; } = "info";

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                _logger.LogInformation("Loading tasks");
                var tasks = await _taskService.GetAllTasksAsync() ?? new List<TaskItem>();
                Tasks = tasks.OrderBy(t => t.DueDate).ThenBy(t => t.Name).ToList();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading tasks");
                StatusMessage = "Error loading tasks. Please try again later.";
                StatusMessageType = "danger";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostMarkCompletedAsync(Guid id, bool isCompleted = true)
        {
            try
            {
                _logger.LogInformation("Updating task completion status. TaskId: {TaskId}, IsCompleted: {IsCompleted}", id, isCompleted);
                
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null)
                {
                    _logger.LogWarning("Task not found. TaskId: {TaskId}", id);
                    StatusMessage = "Task not found.";
                    StatusMessageType = "warning";
                    return RedirectToPage();
                }

                task.IsCompleted = isCompleted;
                task.DueDate = task.DueDate.Date; // Ensure time part is removed
                await _taskService.UpdateTaskAsync(task);
                
                _logger.LogInformation("Task {TaskId} marked as {Status}", id, isCompleted ? "completed" : "not completed");
                StatusMessage = $"Task '{task.Name}' marked as {(isCompleted ? "completed" : "not completed")}.";
                StatusMessageType = "success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task status. TaskId: {TaskId}", id);
                StatusMessage = "Error updating task status. Please try again.";
                StatusMessageType = "danger";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting task. TaskId: {TaskId}", id);
                
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null)
                {
                    _logger.LogWarning("Task not found for deletion. TaskId: {TaskId}", id);
                    StatusMessage = "Task not found.";
                    StatusMessageType = "warning";
                    return RedirectToPage();
                }

                await _taskService.DeleteTaskAsync(id);
                _logger.LogInformation("Task deleted successfully. TaskId: {TaskId}", id);
                
                StatusMessage = $"Task '{task.Name}' has been deleted.";
                StatusMessageType = "success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task. TaskId: {TaskId}", id);
                StatusMessage = "Error deleting task. Please try again.";
                StatusMessageType = "danger";
            }

            return RedirectToPage();
        }
    }
}