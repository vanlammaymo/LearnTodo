using System;
using System.Threading.Tasks;
using Todo.Todos;

namespace Todo.Domain.Services;

public interface ITodoManager
{
    Task<TodoItem> CreateAsync(
        Guid currentUserId,
        string title,
        string description,
        DateTime dueDate,
        Priority priority = Priority.Medium,
        bool isDone = false);
    Task<TodoItem> UpdateAsync(
        Guid id,
        string title,
        string description,
        DateTime dueDate,
        Priority priority,
        bool isDone);
}
