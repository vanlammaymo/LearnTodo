using Todo.Todos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using Volo.Abp.Authorization.Permissions;
using System.Collections.Generic;
using System.Linq;

namespace Todo.Identity;

public class IdentitySeedDataContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IdentityUserManager _userManager;
    private readonly IdentityRoleManager _roleManager;
    private readonly IPermissionManager _permissionManager;
    private readonly IGuidGenerator _guidGenerator;
    private readonly PermissionDefinitionManager _permissionDefinitionManager;

    public IdentitySeedDataContributor(
        IdentityUserManager userManager,
        IdentityRoleManager roleManager,
        IPermissionManager permissionManager,
        IGuidGenerator guidGenerator,
        PermissionDefinitionManager permissionDefinitionManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _permissionManager = permissionManager;
        _guidGenerator = guidGenerator;
        _permissionDefinitionManager = permissionDefinitionManager;
    }

    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedRolesAsync();
        await SeedPermissionsAsync();
        await SeedUsersAsync();
    }

    public async Task SeedRolesAsync()
    {
        // Seed Admin role
        if (await _roleManager.FindByNameAsync(Roles.Admin) == null)
        {

            var adminRole = new IdentityRole(_guidGenerator.Create(), Roles.Admin);
            await _roleManager.CreateAsync(adminRole);

        }
        // Seed User role
        if (await _roleManager.FindByNameAsync(Roles.User) == null)
        {
            var userRole = new IdentityRole(_guidGenerator.Create(), Roles.User);
            userRole.IsDefault = true;
            userRole.IsPublic = true;
            await _roleManager.CreateAsync(userRole);
        }
    }

    public async Task SeedUsersAsync()
    {
        var admins = await _userManager.GetUsersInRoleAsync(Roles.Admin);
        if (admins.Count <= 0)
        {
            IdentityUser admin = new IdentityUser(
                _guidGenerator.Create(),
                "admin",
                "admin@abp.io"
            );
            await _userManager.CreateAsync(admin, "AdminPass");
        }
        var users = await _userManager.GetUsersInRoleAsync(Roles.User);
        if (users.Count <= 0)
        {
            IdentityUser user = new IdentityUser(
                _guidGenerator.Create(),
                "user",
                "user@abp.io"
            );
            await _userManager.CreateAsync(user, "UserPass");
        }
    }

    public async Task SeedPermissionsAsync()
    {
        // Seed permissions for Admin role
        var allPermissions = await _permissionDefinitionManager.GetPermissionsAsync();

        foreach (var permission in allPermissions)
        {
            if (permission.Providers.Count > 0 && !permission.Providers.Contains(RolePermissionValueProvider.ProviderName))
            {
                continue;
            }
            await _permissionManager.SetForRoleAsync(Roles.Admin, permission.Name, true);
        }

        // Seed permissions for User role
        var userPermissions = new[]
        {
            "Todo.Todos.View",
            "Todo.Todos.Create",
            "Todo.Todos.Edit",
            "Todo.Todos.Delete",
            "Todo.Todos.MarkAsDone"
        };

        foreach (string permission in userPermissions)
        {
            await _permissionManager.SetForRoleAsync(Roles.User, permission, true);
        }
    }
}
