using System;
using Todo.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Todo.Todos;

public class TodoItemRepository : EfCoreRepository<TodoDbContext, TodoItem, Guid>, ITodoItemRepository
{
    public TodoItemRepository(IDbContextProvider<TodoDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }
}
