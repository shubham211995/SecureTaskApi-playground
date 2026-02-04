using Microsoft.AspNetCore.Authorization;
using SecureTask.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SecureTask.Api.Authorization;

public class TaskOwnerHandler : AuthorizationHandler<TaskOwnerRequirement, string>
{
    private readonly ITaskRepository _taskRepository;

    public TaskOwnerHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        TaskOwnerRequirement requirement,
        string taskId)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? context.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (userId == null)
            return;

        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
            return;

        if (task.OwnerUserId == userId)
            context.Succeed(requirement);
    }
}