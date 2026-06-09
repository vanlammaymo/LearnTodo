using Microsoft.Extensions.Localization;
using Todo.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Todo.Blazor;

[Dependency(ReplaceServices = true)]
public class TodoBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TodoResource> _localizer;

    public TodoBrandingProvider(IStringLocalizer<TodoResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
