using System.ComponentModel.DataAnnotations;
using TaskTrackR.Api.Models;

namespace TaskTrackR.Api.DTOs;

public class PatchTaskRequest
{
    [MaxLength(200,ErrorMessage = "Title can not exceed 200 characters.")]
    [RegularExpression(@"^[^<>{}]*$", ErrorMessage = "Title cannot contain special characters.")]
    public string? Title { get; set; }
    [MaxLength(1000, ErrorMessage ="Description can not exceed 1000 characters.")]
    public string? Description { get; set; }
    [EnumDataType(typeof(WorkStatus), ErrorMessage = "Invalid status value.")]
    public WorkStatus? Status { get; set; }
    [EnumDataType(typeof(TaskPriority), ErrorMessage = "Invalid priority value.")]
    public TaskPriority? Priority { get; set; }
}