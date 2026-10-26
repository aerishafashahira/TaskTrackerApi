using System.Linq;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Data;

public static class TaskTrackerStore
{
    public static List<TaskItem> Tasks { get; } = new()
    {
        new TaskItem
        {
            Id = 1,
            ProjectId = 1,
            Title = "Create wireframes",
            Description = "Prepare website layouts",
            Status = "Todo"
        },
        new TaskItem
        {
            Id = 2,
            ProjectId = 1,
            Title = "Design homepage",
            Description = "Create homepage design",
            Status = "InProgress"
        },
        new TaskItem
        {
            Id = 3,
            ProjectId = 2,
            Title = "Build login page",
            Description = "Create login interface",
            Status = "Todo"
        },
        new TaskItem
        {
            Id = 4,
            ProjectId = 2,
            Title = "Test login form",
            Description = "Test login validation",
            Status = "Done"
        }
    };

    public static List<Project> Projects { get; } = new()
    {
        new Project
        {
            Id = 1,
            Name = "Website Redesign",
            Description = "Redesign the company website",
            Tasks = Tasks.Where(t => t.ProjectId == 1).ToList()
        },
        new Project
        {
            Id = 2,
            Name = "Job Application Portal",
            Description = "Build a job application portal",
            Tasks = Tasks.Where(t => t.ProjectId == 2).ToList()
        }
    };
}