
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.Models;

public class Project
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Project name is required.")]
    [StringLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Project description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<TaskItem> Tasks { get; set; } = new();
}
