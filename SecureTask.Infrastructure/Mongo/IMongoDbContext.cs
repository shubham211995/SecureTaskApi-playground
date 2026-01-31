using MongoDB.Driver;

public interface IMongoDbContext
{
    IMongoCollection<TaskItem> Tasks { get; }
}