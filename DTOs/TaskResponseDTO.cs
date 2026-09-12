using System.ComponentModel.DataAnnotations;

public class TaskResponseDTO
{
    public int Id{get;set;}
    [Required]
    public string? Title{get;set;}
    public string? Description{get;set;}
    public ItemStatus Status{get;set;}
    public DateTime Deadline{get;set;}
    public DateTime CreationDate{get;set;}
    public bool IsOverdue{get;set;}
}