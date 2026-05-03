namespace TaskTrackR.Api.Models;

public enum WorkStatus   { Todo, InProgress, Done }
public enum TaskPriority { Low, Medium, High }

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public WorkStatus Status { get; set; } = WorkStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
