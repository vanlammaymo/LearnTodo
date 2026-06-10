using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Todo;
using Todo.Domain.Services;
using Todo.Todos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

[Authorize]
public class TodoItemAppService : ApplicationService, ITodoItemAppService
{
    private readonly ITodoManager _todoManager;
    private readonly IRepository<TodoItem, Guid> _todoRepository;
    private readonly TodoApplicationMappers _mappers;
    public TodoItemAppService(ITodoManager todoManager, TodoApplicationMappers mappers, IRepository<TodoItem, Guid> todoRepository)
    {
        _todoManager = todoManager;
        _mappers = mappers;
        _todoRepository = todoRepository;
    }

    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
    {
        Guid currentUserId = CurrentUser.GetId();
        TodoItem newItem = await _todoManager.CreateAsync(
            currentUserId,
            input.Title,
            input.Description,
            input.DueDate,
            input.IsDone
        );

        await _todoRepository.InsertAsync(newItem);

        return _mappers.Map(newItem);
    }
}