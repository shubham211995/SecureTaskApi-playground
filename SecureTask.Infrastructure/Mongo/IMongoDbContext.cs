using MongoDB.Driver;
using SecureTask.Domain.Entities;

public interface IMongoDbContext
{
    IMongoCollection<TaskItem> Tasks { get; }
}