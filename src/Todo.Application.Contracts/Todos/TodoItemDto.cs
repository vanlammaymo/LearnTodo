using System;
using Volo.Abp.Application.Dtos;

namespace Todo.Todos.Dto;

public class TodoItemDto : AuditedEntityDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }
}
