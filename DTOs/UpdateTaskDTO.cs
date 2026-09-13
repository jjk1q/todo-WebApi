using System.ComponentModel.DataAnnotations;

public class UpdateTaskDTO
{
    public string? Title{get;set;}
    public string? Description{get;set;}
    public DateTime Deadline{get;set;}
    public ItemStatus Status{get;set;}
}