using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ITaskService _taskService;

        public DeleteModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [BindProperty]
        public TaskItem Task { get; set; } = new TaskItem();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Task = await _taskService.GetTaskByIdAsync(id);
            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToPage("./Index");
        }
    }
}