using Todo.Localization;
using Volo.Abp.Application.Services;

namespace Todo;

/* Inherit your application services from this class.
 */
public abstract class TodoAppService : ApplicationService
{
    protected TodoAppService()
    {
        LocalizationResource = typeof(TodoResource);
    }
}
