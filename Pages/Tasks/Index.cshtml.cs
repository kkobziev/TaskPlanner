using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IStringLocalizer<IndexModel> _localizer;

        public IndexModel(ITaskService taskService, IStringLocalizer<IndexModel> localizer)
        {
            _taskService = taskService;
            _localizer = localizer;
        }

        public IList<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public async Task OnGetAsync()
        {
            Tasks = (await _taskService.GetAllTasksAsync()).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMarkCompletedAsync(Guid id)
        {
            await _taskService.MarkTaskAsCompletedAsync(id);
            return RedirectToPage();
        }
    }
}