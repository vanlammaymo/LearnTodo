using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Todo.Todos;

public class TodoItem : AuditedAggregateRoot<Guid>
{
    public string Title { get; protected set; }

    public string Description { get; protected set; }

    public DateTime? DueDate { get; protected set; }

    public Priority Priority { get; protected set; }

    public bool IsDone { get; protected set; }

    protected TodoItem()
    {

    }

    public TodoItem(Guid id, string title, string description, bool isDone = false, DateTime? dueDate = null, Priority priority = Priority.Medium) : base(id)
    {
        Id = id;
        Title = title;
        Description = description;
        IsDone = isDone;
        Priority = priority;
        dueDate ??= DateTime.Now.AddDays(7);
        DueDate = dueDate;
    }
}
