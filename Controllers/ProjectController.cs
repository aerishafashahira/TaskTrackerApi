
using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Data;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    // GET: /api/projects
    [HttpGet]
    public ActionResult<List<Project>> GetProjects()
    {
        return Ok(TaskTrackerStore.Projects);
    }

    // GET: /api/projects/1
    [HttpGet("{id:int}")]
    public ActionResult<Project> GetProject(int id)
    {
        var project = TaskTrackerStore.Projects
            .FirstOrDefault(p => p.Id == id);

        if (project == null)
        {
            return NotFound(new
            {
                message = $"Project with ID {id} was not found."
            });
        }

        return Ok(project);
    }

    // POST: /api/projects
    [HttpPost]
    public ActionResult<Project> CreateProject(Project request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new
            {
                message = "Project name is required."
            });
        }

        int newId = TaskTrackerStore.Projects.Count == 0
            ? 1
            : TaskTrackerStore.Projects.Max(p => p.Id) + 1;

        var project = new Project
        {
            Id = newId,
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            Tasks = new List<TaskItem>()
        };

        TaskTrackerStore.Projects.Add(project);

        return CreatedAtAction(
            nameof(GetProject),
            new { id = project.Id },
            project
        );
    }

    // PUT: /api/projects/1
    [HttpPut("{id:int}")]
    public ActionResult<Project> UpdateProject(int id, Project request)
    {
        var project = TaskTrackerStore.Projects
            .FirstOrDefault(p => p.Id == id);

        if (project == null)
        {
            return NotFound(new
            {
                message = $"Project with ID {id} was not found."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new
            {
                message = "Project name is required."
            });
        }

        project.Name = request.Name;
        project.Description = request.Description;

        return Ok(project);
    }

    // DELETE: /api/projects/1
    [HttpDelete("{id:int}")]
    public IActionResult DeleteProject(int id)
    {
        var project = TaskTrackerStore.Projects
            .FirstOrDefault(p => p.Id == id);

        if (project == null)
        {
            return NotFound(new
            {
                message = $"Project with ID {id} was not found."
            });
        }

        // Delete the tasks belonging to this project too.
        TaskTrackerStore.Tasks.RemoveAll(t => t.ProjectId == id);

        TaskTrackerStore.Projects.Remove(project);

        return NoContent();
    }
}
