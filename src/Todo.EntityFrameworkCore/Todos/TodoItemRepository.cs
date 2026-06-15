using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Todo.Todos;

public class TodoItemRepository : EfCoreRepository<TodoDbContext, TodoItem, Guid>, ITodoItemRepository
{
    public TodoItemRepository(IDbContextProvider<TodoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    private async Task<IQueryable<TodoItem>> BuildQueryAsync(
        Guid? creatorId,
        string? filterText,
        Priority? priority,
        DateTime? dueDateFrom,
        DateTime? dueDateTo,
        bool? isDone
    )
    {
        var query = await GetQueryableAsync();

        if (creatorId.HasValue)
        {
            query = query.Where(x => x.CreatorId == creatorId);
        }

        if (!string.IsNullOrWhiteSpace(filterText))
        {
            query = query.Where(x =>
                x.Title.Contains(filterText) ||
                x.Description.Contains(filterText));
        }

        if (priority.HasValue)
        {
            query = query.Where(x => x.Priority == priority.Value);
        }

        if (dueDateFrom.HasValue)
        {
            query = query.Where(x => x.DueDate >= dueDateFrom.Value);
        }

        if (dueDateTo.HasValue)
        {
            query = query.Where(x => x.DueDate <= dueDateTo.Value);
        }

        if (isDone.HasValue)
        {
            query = query.Where(x => x.IsDone == isDone.Value);
        }

        return query;
    }

    public async Task<long> GetCountAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        bool? isDone = null,
        CancellationToken cancellationToken = default)
    {
        var query = await BuildQueryAsync(
            creatorId,
            filterText,
            priority,
            dueDateFrom,
            dueDateTo,
            isDone
        );

        return await query.LongCountAsync(cancellationToken);
    }

    public async Task<List<TodoItem>> GetListAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        bool? isDone = null,
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
            dueDateTo,
            isDone);

        query = query.OrderBy(
            string.IsNullOrWhiteSpace(sorting) ?
            "DueDate asc" :
            sorting
        );

        return await query
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(cancellationToken);
    }

}
