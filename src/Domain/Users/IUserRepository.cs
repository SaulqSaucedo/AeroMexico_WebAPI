namespace Domain.Users;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User?> GetByIdAsync(UserId id);
    Task<bool> ExistsAsync(UserId id);
    void Add(User customer);
    void Update(User customer);
    void Delete(User customer);
}