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
    private readonly IRepository<TodoItem, Guid> _todoRepository;
    private readonly IGuidGenerator _guidGenerator;

    public TodoManager(IRepository<TodoItem, Guid> todoRepository, IGuidGenerator guidGenerator)
    {
        _todoRepository = todoRepository;
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
        var existTodoItem = await _todoRepository.FirstOrDefaultAsync(x => x.Title == title && x.CreatorId == currentUserId);
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
        Guid id,
        string title,
        string description,
        DateTime dueDate,
        Priority priority,
        bool isDone)
    {
        var todoItem = await _todoRepository.FindAsync(id);
        if (todoItem == null)
        {
            throw new BusinessException(message: "Todo item not found.");
        }

        todoItem.Update(title, description, dueDate, isDone, todoItem.Priority);

        await _todoRepository.UpdateAsync(todoItem);

        return todoItem;
    }
}