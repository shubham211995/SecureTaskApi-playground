using MongoDB.Driver;
using SecureTask.Domain.Entities;
using SecureTask.Domain.Interfaces;
using SecureTask.Infrastructure.Mongo;

namespace SecureTask.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskItem> _tasks;

    public TaskRepository(MongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _tasks = database.GetCollection<TaskItem>("Tasks");
    }

    public async Task CreateAsync(TaskItem task)
        => await _tasks.InsertOneAsync(task);

    public async Task<TaskItem?> GetByIdAsync(string id)
        => await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();

    public async Task<List<TaskItem>> GetByOwnerAsync(string ownerUserId)
        => await _tasks.Find(t => t.OwnerUserId == ownerUserId).ToListAsync();

    public async Task<List<TaskItem>> GetAllAsync()
        => await _tasks.Find(_ => true).ToListAsync();

    public async Task UpdateAsync(TaskItem task)
        => await _tasks.ReplaceOneAsync(t => t.Id == task.Id, task);

    public async Task DeleteAsync(string id)
        => await _tasks.DeleteOneAsync(t => t.Id == id);
}