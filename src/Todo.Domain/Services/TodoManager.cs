using System;
using System.Threading.Tasks;
using Todo.Todos;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Todo.Errors;

namespace Todo.Domain.Services;

public class TodoManager : DomainService, ITodoManager
{
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly IGuidGenerator _guidGenerator;

    public TodoManager(ITodoItemRepository todoItemRepository, IGuidGenerator guidGenerator)
    {
        _todoItemRepository = todoItemRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task<TodoItem> CreateAsync(
        Guid currentUserId,
        string title,
        string description,
        DateTime dueDate,
        Priority priority,
        bool isDone = false)
    {
        var existTodoItem = await _todoItemRepository.FirstOrDefaultAsync(x => x.Title == title && x.CreatorId == currentUserId);
        if (existTodoItem != null)
        {
            throw new BusinessException(code: TodoErrorCodes.TodoTitleAlreadyExists);
        }
        TodoItem newTodoItem = new TodoItem(
            id: _guidGenerator.Create(),
            title: title,
            description: description,
            dueDate: dueDate,
            priority: priority,
            isDone: isDone
        );
        return newTodoItem;
    }

    public async Task<TodoItem> UpdateAsync(
        TodoItem item,
        string? title,
        string? description,
        DateTime? dueDate,
        Priority? priority)
    {
        // Check if another item already matches the  title
        if (title is not null)
        {
            var todoItem = await _todoItemRepository.FirstOrDefaultAsync(x => x.Id != item.Id &&
                x.CreatorId == item.CreatorId &&
                x.Title == title);

            if (todoItem != null)
            {
                throw new BusinessException(code: TodoErrorCodes.TodoTitleAlreadyExists);
            }
        }

        return item.Update(
            title,
            description,
            dueDate.HasValue ? Clock.Normalize(dueDate.Value) : null,
            priority.HasValue ? priority.Value : null
        );
    }
}
