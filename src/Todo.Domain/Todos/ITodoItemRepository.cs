using System;
using Volo.Abp.Domain.Repositories;

namespace Todo.Todos;

public interface ITodoItemRepository : IRepository<TodoItem, Guid>
{

}
