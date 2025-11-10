using Domain.Users;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(User customer) => _context.Users.Add(customer);
    public void Delete(User customer) => _context.Users.Remove(customer);
    public void Update(User customer) => _context.Users.Update(customer);
    public async Task<bool> ExistsAsync(UserId id) => await _context.Users.AnyAsync(customer => customer.Id == id);
    public async Task<User?> GetByIdAsync(UserId id) => await _context.Users.SingleOrDefaultAsync(c => c.Id == id);
    public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();
}