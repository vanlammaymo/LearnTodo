using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Todo;
using Todo.Domain.Services;
using Todo.Errors;
using Todo.Permissions;
using Todo.Todos;
using Todo.Todos.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

[Authorize]
public class TodoItemAppService : ApplicationService, ITodoItemAppService
{
    private readonly ITodoManager _todoManager;
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly ITodoQueryRepository _todoQueryRepository;
    private readonly TodoApplicationMappers _mappers;
    public TodoItemAppService(
        ITodoManager todoManager,
        TodoApplicationMappers mappers,
        ITodoItemRepository todoItemRepository,
        ITodoQueryRepository todoQueryRepository)
    {
        _todoManager = todoManager;
        _mappers = mappers;
        _todoItemRepository = todoItemRepository;
        _todoQueryRepository = todoQueryRepository;
    }

    [Authorize(TodoPermissions.Todos.Create)]
    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto input)
    {
        Guid currentUserId = CurrentUser.GetId();

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

    public async Task<TodoItemDto> UpdateAsync(UpdateTodoItemDto input)
    {
        TodoItem todoItem = await _todoItemRepository.GetAsync(input.Id);

        // Check admin or owner update the task
        var authorizationResult = await AuthorizationService.AuthorizeAsync(todoItem, "OwnerOrAdminPolicy");

        if (!authorizationResult.Succeeded)
        {
            throw new BusinessException(code: TodoErrorCodes.DontHavePermission);
        }

        TodoItem newTodoItem = await _todoManager.UpdateAsync(
            todoItem,
            input.Title,
            input.Description,
            input.DueDate.HasValue ? Clock.Normalize(input.DueDate.Value) : null,
            input.Priority.HasValue ? input.Priority.Value : null);

        await _todoItemRepository.UpdateAsync(newTodoItem);

        return _mappers.Map(newTodoItem);
    }

    [Authorize(TodoPermissions.Todos.View)]
    public async Task<PagedResultDto<TodoItemDto>> GetListAsync(GetTodoListDto input)
    {
        var currentUserId = CurrentUser.GetId();

        long totalCount;
        List<TodoItem> items;

        if (CurrentUser.IsInRole(Roles.Admin))
        {
            totalCount = await _todoQueryRepository.GetCountAsync(
                input.CreatorId,
                input.FilterText,
                input.Priority,
                input.DueDateFrom,
                input.DueDateTo);

            var itemsWithCreatorInfo = await _todoQueryRepository.GetListWithCreatorInfoAsync(
                input.CreatorId,
                input.FilterText,
                input.Priority,
                input.DueDateFrom,
                input.DueDateTo,
                input.Sorting,
                input.SkipCount ?? 0,
                input.MaxResultCount ?? int.MaxValue
            );
            return new PagedResultDto<TodoItemDto>(
                totalCount,
                _mappers.Map(itemsWithCreatorInfo)
            );
        }
        else
        {
            totalCount = await _todoItemRepository.GetCountAsync(
                currentUserId,
                input.FilterText,
                input.Priority,
                input.DueDateFrom,
                input.DueDateTo);

            items = await _todoItemRepository.GetListAsync(
                currentUserId,
                input.FilterText,
                input.Priority,
                input.DueDateFrom,
                input.DueDateTo,
                input.Sorting,
                input.SkipCount ?? 0,
                input.MaxResultCount ?? int.MaxValue);

            return new PagedResultDto<TodoItemDto>(
                totalCount,
                _mappers.Map(items)
            );
        }
    }
}
