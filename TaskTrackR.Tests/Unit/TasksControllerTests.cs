using Moq;
using Microsoft.AspNetCore.Mvc;
using TaskTrackR.Api.Controllers;
using TaskTrackR.Api.DTOs;
using TaskTrackR.Api.Models;
using TaskTrackR.Api.Services.Interfaces;

namespace TaskTrackR.Tests.Unit;

public class TasksControllerTests
{
    private readonly Mock<ITaskService> _mockService;
    private readonly TasksController _controller;

    public TasksControllerTests()
    {
        _mockService = new Mock<ITaskService>();
        _controller = new TasksController(_mockService.Object);
    }

    // GET /api/tasks
    [Fact]
    public async Task GetAll_Returns200_WithTaskList()
    {
        var tasks = new List<TaskItem>
        {
            new() { Id = 1, Title = "Task 1" },
            new() { Id = 2, Title = "Task 2" }
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(tasks);

        var result = await _controller.GetAll() as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(tasks, result.Value);
    }

    // GET /api/tasks/{id} - found
    [Fact]
    public async Task GetById_Returns200_WhenFound()
    {
        var task = new TaskItem { Id = 1, Title = "Task 1" };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(task);

        var result = await _controller.GetById(1) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(task, result.Value);
    }

    // GET /api/tasks/{id} - not found
    [Fact]
    public async Task GetById_Returns404_WhenNotFound()
    {
        _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((TaskItem?)null);

        var result = await _controller.GetById(99);

        Assert.IsType<NotFoundResult>(result);
    }

    // POST /api/tasks
    [Fact]
    public async Task Create_Returns201_WithCreatedTask()
    {
        var request = new CreateTaskRequest { Title = "New Task", Priority = TaskPriority.Medium };
        var created = new TaskItem { Id = 1, Title = "New Task", Priority = TaskPriority.Medium };
        _mockService.Setup(s => s.CreateAsync(request)).ReturnsAsync(created);

        var result = await _controller.Create(request) as CreatedAtActionResult;

        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(created, result.Value);
    }

    // PATCH /api/tasks/{id} - success
    [Fact]
    public async Task Patch_Returns200_WhenUpdated()
    {
        var request = new PatchTaskRequest { Title = "Updated" };
        var updated = new TaskItem { Id = 1, Title = "Updated" };
        _mockService.Setup(s => s.PatchAsync(1, request)).ReturnsAsync(updated);

        var result = await _controller.Patch(1, request) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(updated, result.Value);
    }

    // PATCH /api/tasks/{id} - not fouind
    [Fact]
    public async Task Patch_Returns404_WhenNotFound()
    {
        var request = new PatchTaskRequest { Title = "Updated" };
        _mockService.Setup(s => s.PatchAsync(99, request)).ReturnsAsync((TaskItem?)null);

        var result = await _controller.Patch(99, request);

        Assert.IsType<NotFoundResult>(result);
    }

    // PUT /api/tasks/{id} - success
    [Fact]
    public async Task Update_Returns200_WhenUpdated()
    {
        var task = new TaskItem { Id = 1, Title = "Updated" };
        _mockService.Setup(s => s.UpdateAsync(1, task)).ReturnsAsync(task);

        var result = await _controller.Update(1, task) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    // PUT /api/tasks/{id} - not found
    [Fact]
    public async Task Update_Returns404_WhenNotFound()
    {
        var task = new TaskItem { Id = 99, Title = "Ghost" };
        _mockService.Setup(s => s.UpdateAsync(99, task)).ReturnsAsync((TaskItem?)null);

        var result = await _controller.Update(99, task);

        Assert.IsType<NotFoundResult>(result);
    }

    // DELETE /api/tasks/{id} - success
    [Fact]
    public async Task Delete_Returns204_WhenDeleted()
    {
        _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    // DELETE /api/tasks/{id} - not found
    [Fact]
    public async Task Delete_Returns404_WhenNotFound()
    {
        _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

        var result = await _controller.Delete(99);

        Assert.IsType<NotFoundResult>(result);
    }
}