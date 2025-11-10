using Domain.Primitives;

namespace Domain.Users;

public sealed class User : AggregateRoot
{
    public User(UserId id, string name, string lastName, string email, 
    bool active)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Active = active;
    }

    private User() { }

    public UserId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{Name} {LastName}";
    public string Email { get; private set; } = string.Empty;
    public bool Active { get; private set; }

    public static User UpdateCustomer(Guid id, string name, string lastName, string email, bool active)
    {
        return new User(new UserId(id), name, lastName, email, active);
    }
}