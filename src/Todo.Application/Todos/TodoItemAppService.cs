using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Todo;
using Todo.Domain.Services;
using Todo.Permissions;
using Todo.Todos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

[Authorize]
public class TodoItemAppService : ApplicationService, ITodoItemAppService
{
    private readonly ITodoManager _todoManager;
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly TodoApplicationMappers _mappers;
    public TodoItemAppService(ITodoManager todoManager,
        TodoApplicationMappers mappers,
        ITodoItemRepository todoItemRepository)
    {
        _todoManager = todoManager;
        _mappers = mappers;
        _todoItemRepository = todoItemRepository;
    }

    [Authorize(TodoPermissions.Todos.Create)]
    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
    {
        Guid currentUserId = CurrentUser.GetId();
        TodoItem newItem = await _todoManager.CreateAsync(
            currentUserId,
            input.Title,
            input.Description,
            input.DueDate,
            input.Priority
        );

        await _todoItemRepository.InsertAsync(newItem);

        return _mappers.Map(newItem);
    }

    // public async Task<TodoItemDto> GetAsync(Guid id)
    // {

    // }
}
