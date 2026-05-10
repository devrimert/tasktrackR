using System.ComponentModel.DataAnnotations;
using TaskTrackR.Api.Models;

namespace TaskTrackR.Api.DTOs;

public class CreateTaskRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title can not exceed 200 characters.")]
    [RegularExpression(@"^[^<>{}]*$", ErrorMessage = "Title can not contain special characters.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000, ErrorMessage = "Description can not exceed 1000 characters.")]
    public string? Description { get; set; }

    [EnumDataType(typeof(WorkStatus), ErrorMessage = "Invalid status value.")]
    public WorkStatus Status { get; set; } = WorkStatus.Todo;

    [EnumDataType(typeof(TaskPriority), ErrorMessage = "Invalid priority value.")]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
}