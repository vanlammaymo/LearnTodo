using System.Threading.Tasks;
using Todo.Localization;
using Todo.Permissions;
using Todo.MultiTenancy;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.Identity.Blazor;

namespace Todo.Blazor.Menus;

public class TodoMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<TodoResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                TodoMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 1
            )
        );

        context.Menu.Items.Add(new ApplicationMenuItem(
            TodoMenus.Todos,
            l["Menu:Todos"],
            "/todos",
            icon: "fa fa-list-check",
            order: 1,
            requiredPermissionName: TodoPermissions.Todos.View
        ));

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 6;

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}
