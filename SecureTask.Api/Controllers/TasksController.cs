using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureTask.Api.DTOs;
using SecureTask.Domain.Entities;
using SecureTask.Domain.Interfaces;
using System.Security.Claims;

namespace SecureTask.Api.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize] // 🔐 all endpoints require authentication
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TasksController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                     ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub)!;

        var task = new TaskItem
        {
            Id = Guid.NewGuid().ToString(),
            Title = request.Title,
            Description = request.Description,
            OwnerUserId = userId,
            IsCompleted = false
        };

        await _taskRepository.CreateAsync(task);
        return Ok(task);
    }

    [HttpGet("mine")]
    public async Task<IActionResult> GetMyTasks()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)!;
        var tasks = await _taskRepository.GetByOwnerAsync(userId);
        return Ok(tasks);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllTasks()
    {
        return Ok(await _taskRepository.GetAllAsync());
    }
}