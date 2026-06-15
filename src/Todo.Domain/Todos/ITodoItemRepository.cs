using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Todo.Todos;

public interface ITodoItemRepository : IRepository<TodoItem, Guid>
{
    Task<long> GetCountAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        bool? isDone = null,
        CancellationToken cancellationToken = default);

    Task<List<TodoItem>> GetListAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        bool? isDone = null,
        string? sorting = null,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        CancellationToken cancellationToken = default);
}
