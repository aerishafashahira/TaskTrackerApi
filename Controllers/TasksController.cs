
using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api")]
public class TasksController : ControllerBase
{
    private static readonly string[] AllowedStatuses =
    {
        "Todo",
        "InProgress",
        "Done"
    };

    // GET: /api/projects/1/tasks
    // Get all tasks belonging to a specific project.
    [HttpGet("projects/{projectId:int}/tasks")]
    public ActionResult<List<TaskItem>> GetProjectTasks(int projectId)
    {
        var project = TaskTrackerStore.Projects
            .FirstOrDefault(p => p.Id == projectId);

        if (project == null)
        {
            return NotFound(new
            {
                message = $"Project with ID {projectId} was not found."
            });
        }

        var tasks = TaskTrackerStore.Tasks
            .Where(t => t.ProjectId == projectId)
            .ToList();

        return Ok(tasks);
    }

    // GET: /api/tasks/1
    // Get a task by its ID.
    [HttpGet("tasks/{id:int}")]
    public ActionResult<TaskItem> GetTask(int id)
    {
        var task = TaskTrackerStore.Tasks
            .FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound(new
            {
                message = $"Task with ID {id} was not found."
            });
        }

        return Ok(task);
    }

    // POST: /api/projects/1/tasks
    // Create a new task for a project.
    [HttpPost("projects/{projectId:int}/tasks")]
    public ActionResult<TaskItem> CreateTask(
        int projectId,
        TaskItem request)
    {
        var project = TaskTrackerStore.Projects
            .FirstOrDefault(p => p.Id == projectId);

        if (project == null)
        {
            return NotFound(new
            {
                message = $"Project with ID {projectId} was not found."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest(new
            {
                message = "Task title is required."
            });
        }

        if (!AllowedStatuses.Contains(request.Status))
        {
            return BadRequest(new
            {
                message = "Status must be Todo, InProgress, or Done."
            });
        }

        int newId = TaskTrackerStore.Tasks.Count == 0
            ? 1
            : TaskTrackerStore.Tasks.Max(t => t.Id) + 1;

        var task = new TaskItem
        {
            Id = newId,
            ProjectId = projectId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        TaskTrackerStore.Tasks.Add(task);

        SyncProjectTasks(projectId);

        return CreatedAtAction(
            nameof(GetTask),
            new { id = task.Id },
            task
        );
    }

    // PUT: /api/tasks/1
    [HttpPut("tasks/{id:int}")]
    public ActionResult<TaskItem> UpdateTask(
        int id,
        TaskItem request)
    {
        var task = TaskTrackerStore.Tasks
            .FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound(new
            {
                message = $"Task with ID {id} was not found."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest(new
            {
                message = "Task title is required."
            });
        }

        if (!AllowedStatuses.Contains(request.Status))
        {
            return BadRequest(new
            {
                message = "Status must be Todo, InProgress, or Done."
            });
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.DueDate = request.DueDate;

        SyncProjectTasks(task.ProjectId);

        return Ok(task);
    }

    // DELETE: /api/tasks/1
    // Delete a task by its ID.
    [HttpDelete("tasks/{id:int}")]
    public IActionResult DeleteTask(int id)
    {
        var task = TaskTrackerStore.Tasks
            .FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            return NotFound(new
            {
                message = $"Task with ID {id} was not found."
            });
        }

        int projectId = task.ProjectId;

        TaskTrackerStore.Tasks.Remove(task);

        SyncProjectTasks(projectId);

        return NoContent();
    }

    // Keep Project.Tasks consistent with the main Tasks list.
    private static void SyncProjectTasks(int projectId)
    {
        var project = TaskTrackerStore.Projects
            .FirstOrDefault(p => p.Id == projectId);

        if (project != null)
        {
            project.Tasks = TaskTrackerStore.Tasks
                .Where(t => t.ProjectId == projectId)
                .ToList();
        }
    }
}
