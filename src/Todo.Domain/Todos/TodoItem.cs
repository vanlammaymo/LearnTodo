using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Todo.Todos;

public class TodoItem : AuditedAggregateRoot<Guid>
{
    public string Title { get; protected set; }

    public string Description { get; protected set; }

    public DateTime DueDate { get; protected set; } = DateTime.Now.AddDays(7);

    public Priority Priority { get; protected set; } = Priority.Medium;

    public bool IsDone { get; protected set; } = false;

    protected TodoItem()
    {

    }

    public TodoItem(Guid id, string title, string description, DateTime dueDate, bool isDone, Priority priority = Priority.Medium) : base(id)
    {
        Id = id;
        Title = title;
        Description = description;
        IsDone = isDone;
        Priority = priority;
        DueDate = dueDate;
    }

    public void Update(string title, string description, DateTime dueDate, bool isDone, Priority priority)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        IsDone = isDone;
    }
}
