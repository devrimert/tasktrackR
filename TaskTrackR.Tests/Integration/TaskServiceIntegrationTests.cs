using Microsoft.EntityFrameworkCore;
using TaskTrackR.Api.Data;
using TaskTrackR.Api.DTOs;
using TaskTrackR.Api.Models;
using TaskTrackR.Api.Services;

namespace TaskTrackR.Tests.Integration;

public class TaskServiceIntegrationTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly TaskService _service;

    public TaskServiceIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // new db for each test
            .Options;

        _context = new AppDbContext(options);
        _service = new TaskService(_context);
    }

    [Fact]
    public async Task CreateAsync_SavesTaskToDatabase()
    {
        //arrange
        var request = new CreateTaskRequest { Title = "Integration Task", Priority = TaskPriority.High };

        //act
        var result = await _service.CreateAsync(request);

        //assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Integration Task", result.Title);
        Assert.Equal(1, await _context.Tasks.CountAsync());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSavedTasks()
    {
        //arrange
        _context.Tasks.AddRange(
            new TaskItem { Title = "Task A", Status = WorkStatus.Todo, Priority = TaskPriority.Low },
            new TaskItem { Title = "Task B", Status = WorkStatus.Done, Priority = TaskPriority.High }
        );
        await _context.SaveChangesAsync();

        //act
        var result = await _service.GetAllAsync();

        //assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task PatchAsync_UpdatesOnlyProvidedFields()
    {
        //arrange
        var task = new TaskItem { Title = "Original", Status = WorkStatus.Todo, Priority = TaskPriority.Low };
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        var request = new PatchTaskRequest { Title = "Updated Title" }; // just title

        //act
        var result = await _service.PatchAsync(task.Id, request);

        //assert
        Assert.NotNull(result);
        Assert.Equal("Updated Title", result.Title);    //change
        Assert.Equal(WorkStatus.Todo, result.Status);   // no change
        Assert.Equal(TaskPriority.Low, result.Priority); // no change
    }

    [Fact]
    public async Task DeleteAsync_RemovesTaskFromDatabase()
    {
        //arrange
        var task = new TaskItem { Title = "To Delete", Status = WorkStatus.Todo, Priority = TaskPriority.Low };
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        //act
        var result = await _service.DeleteAsync(task.Id);

        //assert
        Assert.True(result);
        Assert.Equal(0, await _context.Tasks.CountAsync());
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenTaskNotFound()
    {
        //act
        var result = await _service.DeleteAsync(999);

        //assert
        Assert.False(result);
    }

    public void Dispose() => _context.Dispose();
}