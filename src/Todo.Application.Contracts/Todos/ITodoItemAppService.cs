using System.Threading.Tasks;
using Volo.Abp.Application.Services;

public interface ITodoItemAppService : IApplicationService
{
    Task<TodoItemDto> CreateAsync(CreateTodoItemDto input);
}
