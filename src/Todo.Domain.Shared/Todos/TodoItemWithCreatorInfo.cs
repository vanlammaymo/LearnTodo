using System;

namespace Todo.Todos;

public class TodoItemWithCreatorInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; }

    public Guid CreatorId { get; set; }
    public string CreatorUserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
