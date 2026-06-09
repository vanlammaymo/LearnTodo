using Todo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Todo.Permissions;

public class TodoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TodoPermissions.GroupName);

        var todosPermission = myGroup.AddPermission(TodoPermissions.TodosPermissions.Default, L("Permission:Todos"));
        todosPermission.AddChild(TodoPermissions.TodosPermissions.Create);
        todosPermission.AddChild(TodoPermissions.TodosPermissions.Edit);
        todosPermission.AddChild(TodoPermissions.TodosPermissions.Delete);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TodoResource>(name);
    }
}
