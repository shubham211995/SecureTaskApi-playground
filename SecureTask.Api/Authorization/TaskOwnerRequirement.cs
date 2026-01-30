using Microsoft.AspNetCore.Authorization;

namespace SecureTask.Api.Authorization;

public class TaskOwnerRequirement : IAuthorizationRequirement { }