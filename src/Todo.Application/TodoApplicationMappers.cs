using Riok.Mapperly.Abstractions;
using Todo.Todos;
using Todo.Todos.Dto;
using Volo.Abp.Mapperly;

namespace Todo;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class TodoApplicationMappers : MapperBase<TodoItem, TodoItemDto>
{
    public override partial TodoItemDto Map(TodoItem source);

    public override partial void Map(TodoItem source, TodoItemDto destination);
}
