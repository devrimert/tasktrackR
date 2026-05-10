using TaskTrackR.Api.Models;
using TaskTrackR.Api.DTOs;

namespace TaskTrackR.Api.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(CreateTaskRequest request);
    Task<TaskItem?> PatchAsync(int id, PatchTaskRequest request);
    Task<TaskItem?> UpdateAsync(int id, TaskItem updated);
    Task<bool> DeleteAsync(int id);

}