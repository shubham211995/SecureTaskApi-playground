using MongoDB.Driver;
using SecureTask.Domain.Interfaces;

public class UserRepository : IUserRepository
{
    //private readonly IMongoCollection<User> _users;

    public Task CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}