public class MongoDbContext : IMongoDbContext
{
    public IMongoCollection<TaskItem> Tasks { get; }

    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var db = client.GetDatabase(config["MongoDb:Database"]);
        Tasks = db.GetCollection<TaskItem>("Tasks");
    }
}