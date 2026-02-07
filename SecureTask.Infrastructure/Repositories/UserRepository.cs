using MongoDB.Driver;
using SecureTask.Domain.Entities;
using SecureTask.Domain.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDbContext context)
    {
        _users = context.Users;
    }

    public async Task CreateAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users
            .Find(u => u.Email == email)
            .FirstOrDefaultAsync();
    }
}