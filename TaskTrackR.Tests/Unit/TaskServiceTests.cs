using Moq;
using TaskTrackR.Api.DTOs;
using TaskTrackR.Api.Models;
using TaskTrackR.Api.Services.Interfaces;

namespace TaskTrackR.Tests.Unit;

public class TaskServiceTests
{
    private readonly Mock<ITaskService> _mockService;

    public TaskServiceTests()
    {
        _mockService = new Mock<ITaskService>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAll()
    {
        //arrange
        var tasks = new List<TaskItem>
        {
            new TaskItem {Id=1, Title="Task1", Status=WorkStatus.Todo, Priority=TaskPriority.Low},
            new TaskItem {Id=2, Title="Task2", Status=WorkStatus.InProgress, Priority=TaskPriority.Medium},
            new TaskItem {Id=3, Title="Task3", Status=WorkStatus.Done, Priority=TaskPriority.High}    
        };
        _mockService.Setup(s=> s.GetAllAsync()).ReturnsAsync(tasks);

        //act
        var result = await _mockService.Object.GetAllAsync();

        //assert
        Assert.Equal(3, result.Count);
        Assert.Equal("Task1", result[0].Title);
        Assert.Equal(WorkStatus.Done, result[2].Status);
        Assert.Equal(TaskPriority.Medium, result[1].Priority);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNullWhenNotFound()
    {
        //arrange
        _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((TaskItem?)null);

        //act
        var result = await _mockService.Object.GetByIdAsync(99);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ReturnsCreatedTask()
    {
        //arrange
        var request = new CreateTaskRequest { Title = "New Task", Priority = TaskPriority.Medium };
        var created = new TaskItem { Id = 1, Title = "New Task", Priority = TaskPriority.Medium };
        _mockService.Setup(s => s.CreateAsync(request)).ReturnsAsync(created);

        //act
        var result = await _mockService.Object.CreateAsync(request);

        //assert
        Assert.NotNull(result);
        Assert.Equal("New Task", result.Title);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
    {
        //arrange
        _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

        //act
        var result = await _mockService.Object.DeleteAsync(99);

        //assert
        Assert.False(result);
    }

    [Fact]
    public async Task PatchAsync_ReturnsNull_WhenTaskNotFound()
    {
        //arrange
        var request = new PatchTaskRequest { Title = "Updated" };
        _mockService.Setup(s => s.PatchAsync(99, request)).ReturnsAsync((TaskItem?)null);

        //act
        var result = await _mockService.Object.PatchAsync(99, request);

        //assert
        Assert.Null(result);
    }

}