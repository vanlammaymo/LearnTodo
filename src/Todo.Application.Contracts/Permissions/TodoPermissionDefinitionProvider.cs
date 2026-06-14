using Todo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Todo.Permissions;

public class TodoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TodoPermissions.GroupName);

        // myGroup.AddPermission(TodoPermissions.TodosPermissions.Default, L("Permission:Todos"));
        myGroup.AddPermission(TodoPermissions.Todos.View, L("Permission:Todos.View"));
        myGroup.AddPermission(TodoPermissions.Todos.Create, L("Permission:Todos.Create"));
        myGroup.AddPermission(TodoPermissions.Todos.Update, L("Permission:Todos.Update"));
        myGroup.AddPermission(TodoPermissions.Todos.Delete, L("Permission:Todos.Delete"));
        myGroup.AddPermission(TodoPermissions.Todos.MarkAsDone, L("Permission:Todos.MarkAsDone"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TodoResource>(name);
    }
}
