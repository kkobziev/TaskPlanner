using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ITaskService _taskService;

        [BindProperty]
        public TaskItem Task { get; set; } = new TaskItem();

        public EditModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Task = await _taskService.GetTaskByIdAsync(id);
            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskService.UpdateTaskAsync(Task);
            return RedirectToPage("Index");
        }
    }
}