using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.EntityFrameworkCore;
using Todo.Todos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

public class TodoQueryRepository : IScopedDependency, ITodoQueryRepository
{
    private readonly IDbContextProvider<TodoDbContext> _dbContextProvider;

    public TodoQueryRepository(IDbContextProvider<TodoDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    public async Task<IQueryable<TodoItemWithCreatorInfo>> BuildQueryAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await _dbContextProvider.GetDbContextAsync();

        var query =
            from todo in dbContext.TodoItems
            join user in dbContext.Users
                on todo.CreatorId equals user.Id
            select new TodoItemWithCreatorInfo
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsDone = todo.IsDone,
                Priority = todo.Priority,
                DueDate = todo.DueDate,
                CreatorId = user.Id,
                CreatorUserName = user.UserName,
                Email = user.Email
            };

        if (creatorId.HasValue)
        {
            query = query.Where(x =>
                x.CreatorId == creatorId);
        }

        if (!string.IsNullOrWhiteSpace(filterText))
        {
            query = query.Where(x =>
                x.Title.Contains(filterText) ||
                x.Description!.Contains(filterText));
        }

        if (priority.HasValue)
        {
            query = query.Where(x =>
                x.Priority == priority.Value);
        }

        if (dueDateFrom.HasValue)
        {
            query = query.Where(x =>
                x.DueDate >= dueDateFrom.Value);
        }

        if (dueDateTo.HasValue)
        {
            query = query.Where(x =>
                x.DueDate <= dueDateTo.Value);
        }

        return query;
    }

    public async Task<long> GetCountAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        CancellationToken cancellationToken = default)
    {
        var query = await BuildQueryAsync(
            creatorId,
            filterText,
            priority,
            dueDateFrom,
            dueDateTo
        );

        return await query.LongCountAsync(cancellationToken);
    }

    public async Task<List<TodoItemWithCreatorInfo>> GetListWithCreatorInfoAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        string? sorting = null,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        CancellationToken cancellationToken = default)
    {
        var query = await BuildQueryAsync(
            creatorId,
            filterText,
            priority,
            dueDateFrom,
            dueDateTo);

        query = query.OrderBy(
            string.IsNullOrWhiteSpace(sorting)
                ? "DueDate asc"
                : sorting);

        return await query
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(cancellationToken);
    }
}
