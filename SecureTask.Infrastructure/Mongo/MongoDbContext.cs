using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SecureTask.Domain.Entities;

public class MongoDbContext : IMongoDbContext
{
    public IMongoCollection<TaskItem> Tasks { get; }

    IMongoCollection<TaskItem> IMongoDbContext.Tasks => throw new NotImplementedException();

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var db = client.GetDatabase(config["MongoDb:Database"]);
        Tasks = db.GetCollection<TaskItem>("Tasks");
    }
}