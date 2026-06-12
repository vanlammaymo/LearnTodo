using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Todo;
using Todo.Domain.Services;
using Todo.Permissions;
using Todo.Todos;
using Todo.Todos.Dto;
using Volo.Abp.Application.Services;
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

        input.Priority = input.Priority ?? Priority.Medium;
        input.DueDate = input.DueDate ?? Clock.Normalize(DateTime.Now.AddDays(3));

        TodoItem newItem = await _todoManager.CreateAsync(
            currentUserId,
            input.Title,
            input.Description,
            input.DueDate.HasValue ? Clock.Normalize(input.DueDate.Value) : Clock.Normalize(DateTime.Now.AddDays(3)),
            input.Priority.HasValue ? input.Priority.Value : Priority.Medium
        );

        await _todoItemRepository.InsertAsync(newItem);

        return _mappers.Map(newItem);
    }

    // public async Task<TodoItemDto> UpdateAsync(UpdateTodoItemDto input)
    // {
    //     Guid currentUserId = CurrentUser.GetId();


    //     // Logic to grant admin or owner update the task



    // }
}
