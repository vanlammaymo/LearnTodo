using Todo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Todo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TodoController : AbpControllerBase
{
    protected TodoController()
    {
        LocalizationResource = typeof(TodoResource);
    }
}
