using TaskTrackR.Api.Models;

namespace TaskTrackR.Api.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem?> UpdateAsync(int id, TaskItem updated);
    Task<bool> DeleteAsync(int id);

}