using System;
using System.ComponentModel.DataAnnotations;

namespace TaskPlanner.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(1);
        
        public bool IsCompleted { get; set; } = false;
    }
}