using System;
using Todo.Todos;
using Volo.Abp.Domain.Repositories;

public interface ITodoItemRepository : IRepository<TodoItem, Guid>
{

}