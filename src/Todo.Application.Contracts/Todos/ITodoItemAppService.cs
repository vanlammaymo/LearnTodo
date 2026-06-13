using System.Threading.Tasks;
using Todo.Todos.Dto;
using Volo.Abp.Application.Services;

namespace Todo.Todos;

public interface ITodoItemAppService : IApplicationService
{
    Task<TodoItemDto> CreateAsync(CreateTodoItemDto input);
    Task<TodoItemDto> UpdateAsync(UpdateTodoItemDto input);
}
