using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TaskPlanner.Models;
using TaskPlanner.Services;

namespace TaskPlanner.Pages.Tasks
{
    public class CreateModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ITaskService taskService, ILogger<CreateModel> logger)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        [Required(ErrorMessage = "Task name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Task name must be between {2} and {1} characters")]
        public string TaskName { get; set; }

        [BindProperty]
        [StringLength(500, ErrorMessage = "Description cannot exceed {1} characters")]
        public string TaskDescription { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        [FutureDate(ErrorMessage = "Due date must be today or in the future")]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(1);

        public void OnGet()
        {
            // Default values are set in the properties
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid. Errors: {Errors}", 
                    string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
                
                // Log each model state error for debugging
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        _logger.LogWarning("Validation error for {Field}: {Errors}",
                            entry.Key,
                            string.Join(", ", entry.Value.Errors.Select(e => e.ErrorMessage)));
                    }
                }
                
                return Page();
            }

            try
            {
                var newTask = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Name = TaskName.Trim(),
                    Description = string.IsNullOrWhiteSpace(TaskDescription) ? null : TaskDescription.Trim(),
                    DueDate = DueDate.Date, // Ensure no time component
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                };
                
                await _taskService.AddTaskAsync(newTask);
                _logger.LogInformation("Task created successfully. TaskId: {TaskId}", newTask.Id);
                
                TempData["StatusMessage"] = $"Task '{newTask.Name}' has been created successfully.";
                TempData["StatusMessageType"] = "success";
                
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while creating the task. Please try again.");
                return Page();
            }
        }
    }

    // Custom validation attribute for future dates
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Date < DateTime.Today)
                return new ValidationResult(ErrorMessage ?? "Due date cannot be in the past");
            }
            return ValidationResult.Success;
        }
    }
}