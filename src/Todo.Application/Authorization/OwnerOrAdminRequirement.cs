using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Todo.Todos;
using Volo.Abp.Users;

public class OwnerOrAdminRequirement : IAuthorizationRequirement
{

}

public class OwnerOrAdminAuthorizationHandler : AuthorizationHandler<OwnerOrAdminRequirement, TodoItem>
{
    private readonly ICurrentUser _currentUser;

    public OwnerOrAdminAuthorizationHandler(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrAdminRequirement requirement,
        TodoItem resource)
    {
        // If admin => succeed
        if (context.User.IsInRole(Roles.Admin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        // If Owner => succeed
        if (_currentUser.Id.HasValue &&
            resource.CreatorId == _currentUser.Id.Value)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
