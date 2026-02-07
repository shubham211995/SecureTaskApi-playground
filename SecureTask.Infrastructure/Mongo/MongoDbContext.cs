using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SecureTask.Domain.Entities;

public class MongoDbContext : IMongoDbContext
{
    public IMongoCollection<TaskItem> Tasks { get; }
    public IMongoCollection<User> Users { get; }

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MongoSettings:ConnectionString"]);
        var db = client.GetDatabase(config["MongoSettings:DatabaseName"]);

        Tasks = db.GetCollection<TaskItem>("Tasks");
        Users = db.GetCollection<User>("Users");
    }
}