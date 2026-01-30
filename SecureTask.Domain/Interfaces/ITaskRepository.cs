using SecureTask.Domain.Entities;

namespace SecureTask.Domain.Interfaces;

public interface ITaskRepository
{
    Task CreateAsync(TaskItem task);
    Task<TaskItem?> GetByIdAsync(string id);
    Task<List<TaskItem>> GetByOwnerAsync(string ownerUserId);
    Task<List<TaskItem>> GetAllAsync();
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(string id);
}