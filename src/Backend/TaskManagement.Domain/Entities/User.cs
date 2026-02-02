namespace TaskManagement.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    
    public ICollection<TodoItem> TodoItems { get; private set; }

    #pragma warning disable CS8618
    protected User() { }
    #pragma warning disable CS8618

    public User(string firstName, string lastName, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException ("First name cannot be empty");
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException ("Last name cannot be empty");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException ("Email cannot be empty");

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        TodoItems = new List<TodoItem>();
    }
}
