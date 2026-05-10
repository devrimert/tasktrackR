using TaskTrackR.Api.Models;
using Microsoft.AspNetCore.Mvc;
using TaskTrackR.Api.Services.Interfaces;

namespace TaskTrackR.Api.Controllers;

[ApiController]         // Enables automatic model validation - binding from json etc.
[Route("api/[controller]")] 
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService) => _taskService = taskService;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _taskService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        var created = await _taskService.CreateAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, PatchTaskRequest request)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null) return NotFound();

        if (request.Title       is not null) task.Title       = request.Title;
        if (request.Description is not null) task.Description = request.Description;
        if (request.Status      is not null) task.Status      = request.Status.Value;
        if (request.Priority    is not null) task.Priority    = request.Priority.Value;

        var updated = await _taskService.UpdateAsync(id, task);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem updated)
    {
        var task = await _taskService.UpdateAsync(id, updated);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _taskService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}
