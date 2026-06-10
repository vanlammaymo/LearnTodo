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

        // myGroup.AddPermission(TodoPermissions.TodosPermissions.Default, L("Permission:Todos"));
        myGroup.AddPermission(TodoPermissions.TodosPermissions.View, L("Permission:Todos.View"));
        myGroup.AddPermission(TodoPermissions.TodosPermissions.Create, L("Permission:Todos.Create"));
        myGroup.AddPermission(TodoPermissions.TodosPermissions.Edit, L("Permission:Todos.Edit"));
        myGroup.AddPermission(TodoPermissions.TodosPermissions.Delete, L("Permission:Todos.Delete"));
        myGroup.AddPermission(TodoPermissions.TodosPermissions.MarkAsDone, L("Permission:Todos.CheckAsDone"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TodoResource>(name);
    }
}
