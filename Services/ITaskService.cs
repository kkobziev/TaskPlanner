using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Models;

namespace TaskPlanner.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(Guid id);
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(Guid id);
        Task MarkTaskAsCompletedAsync(Guid id);
    }
}