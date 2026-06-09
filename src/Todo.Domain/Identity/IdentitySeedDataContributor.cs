using Todo.Todos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using Volo.Abp.Authorization.Permissions;

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
            await _roleManager.CreateAsync(userRole);
        }
    }

    public async Task SeedUsersAsync()
    {
        // Seed users here
    }

    public async Task SeedPermissionsAsync()
    {
        // Seed permissions for Admin role
        var allPermissions = _permissionDefinitionManager.GetPermissionsAsync().Result;
        foreach (var permission in allPermissions)
        {
            await _permissionManager.SetForRoleAsync(Roles.Admin, permission.Name, true);
        }
    }
}
