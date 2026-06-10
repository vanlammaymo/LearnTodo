using System;
using Todo.Todos;
using Volo.Abp.Application.Dtos;

public class TodoItemDto : AuditedEntityDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public DateTime DueDate { get; set; }
}
