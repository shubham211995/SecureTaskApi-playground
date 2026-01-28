public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;
}