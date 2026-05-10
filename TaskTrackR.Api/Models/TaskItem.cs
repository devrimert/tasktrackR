using System.ComponentModel.DataAnnotations;

namespace TaskTrackR.Api.Models;

public enum WorkStatus   { Todo, InProgress, Done }
public enum TaskPriority { Low, Medium, High }

public class TaskItem
{
    public int Id { get; set; }
    [MaxLength(200,ErrorMessage = "Title can not exceed 200 characters.")]
    public string Title { get; set; } = string.Empty;
    [MaxLength(1000, ErrorMessage ="Description can not exceed 1000 characters.")]
    public string? Description { get; set; }
    public WorkStatus Status { get; set; } = WorkStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
