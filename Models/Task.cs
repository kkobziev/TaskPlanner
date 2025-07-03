using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskPlanner.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.")]
        [DisplayName("Task Name")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        [DisplayName("Description")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "The Due Date field is required.")]
        [DataType(DataType.Date)]
        [DisplayName("Due Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(1);
        
        [DisplayName("Completed")]
        public bool IsCompleted { get; set; } = false;

        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}