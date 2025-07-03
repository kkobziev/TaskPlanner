using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskService _taskService;

        public CreateModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [BindProperty]
        public TaskItem Task { get; set; } = new TaskItem();

        public void OnGet()
        {
            Task = new TaskItem();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _taskService.AddTaskAsync(Task);
            return RedirectToPage("./Index");
        }
    }
}