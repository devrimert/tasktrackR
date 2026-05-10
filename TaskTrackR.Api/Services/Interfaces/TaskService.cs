using TaskTrackR.Api.Data;
using TaskTrackR.Api.Models;
using TaskTrackR.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskTrackR.Api.DTOs;

namespace TaskTrackR.Api.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;
    public TaskService(AppDbContext db) => _db = db;
    public async Task<TaskItem> CreateAsync(CreateTaskRequest request)
    {
        var task = new TaskItem
        {
            Title       = request.Title,
            Description = request.Description,
            Status      = request.Status,
            Priority    = request.Priority
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return false;
        _db.Tasks.Remove(task);
        _db.SaveChanges();
        return true;
    }

    public async Task<List<TaskItem>> GetAllAsync() =>
        await _db.Tasks.ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _db.Tasks.FindAsync(id);

    public async Task<TaskItem?> PatchAsync(int id, PatchTaskRequest request)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task is null) return null;
    
        if (request.Title       is not null) task.Title       = request.Title;
        if (request.Description is not null) task.Description = request.Description;
        if (request.Status      is not null) task.Status      = request.Status.Value;
        if (request.Priority    is not null) task.Priority    = request.Priority.Value;
    
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> UpdateAsync(int id, TaskItem updated)
    {
        var task = await _db.Tasks.FindAsync(id);
        if(task is null) return null;

        task.Title = updated.Title;
        task.Description = updated.Description;
        task.Status = updated.Status;
        task.Priority = updated.Priority;

        await _db.SaveChangesAsync();
        return task;
    }
}