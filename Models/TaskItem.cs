
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.Models;

public class TaskItem
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Task title is required.")]
    [StringLength(200, ErrorMessage = "Task title cannot exceed 200 characters.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Task description cannot exceed 1000 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Task status is required.")]
    public string Status { get; set; } = "Todo";

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
