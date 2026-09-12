using System.ComponentModel.DataAnnotations;

public class CreateTaskDTO
{
    [Required]
    public string? Title{get;set;}
    public string? Description{get;set;}
    public DateTime Deadline{get;set;}
}