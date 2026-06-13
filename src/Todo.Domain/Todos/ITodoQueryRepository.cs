using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Todo.Todos;

public interface ITodoQueryRepository
{
    Task<IQueryable<TodoItemWithCreatorInfo>> BuildQueryAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        CancellationToken cancellationToken = default);

    Task<List<TodoItemWithCreatorInfo>> GetListWithCreatorInfoAsync(
        Guid? creatorId = null,
        string? filterText = null,
        Priority? priority = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null,
        string? sorting = null,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        CancellationToken cancellationToken = default
    );
}
