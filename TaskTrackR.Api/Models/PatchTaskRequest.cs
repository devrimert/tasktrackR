namespace TaskTrackR.Api.Models;

public class PatchTaskRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public WorkStatus? Status { get; set; }
    public TaskPriority? Priority { get; set; }
}