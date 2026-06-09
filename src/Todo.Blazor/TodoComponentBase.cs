using Todo.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Todo.Blazor;

public abstract class TodoComponentBase : AbpComponentBase
{
    protected TodoComponentBase()
    {
        LocalizationResource = typeof(TodoResource);
    }
}
