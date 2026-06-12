using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Timing;

namespace Todo.Todos;

public class TodoItem : AuditedAggregateRoot<Guid>
{
    public string Title { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
    public DateTime DueDate { get; protected set; }
    public Priority Priority { get; protected set; }
    public bool IsDone { get; protected set; } = false;

    protected TodoItem()
    {

    }

    public TodoItem(
        Guid id,
        string title,
        string description,
        DateTime dueDate,
        bool isDone,
        Priority priority) : base(id)
    {
        Id = id;
        Title = title;
        Description = description;
        IsDone = isDone;
        Priority = priority;
        DueDate = dueDate;
    }

    public TodoItem Update(
        string? title = null,
        string? description = null,
        DateTime? dueDate = null,
        Priority? priority = null)
    {
        if (title is not null) Title = title;
        if (description is not null) Description = description;
        if (dueDate.HasValue) DueDate = dueDate.Value;
        if (priority.HasValue) Priority = priority.Value;

        return this;
    }

    public void MarkAsDone()
    {
        if (IsDone)
        {
            return;
        }
        IsDone = true;
    }

    public void ReOpen()
    {
        if (!IsDone)
        {
            return;
        }
        IsDone = false;
    }
}
