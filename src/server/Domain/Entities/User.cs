namespace Domain.Entities;

public class User : EntityBase
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Role> Roles { get; set; } = new();
    public Guid RefreshToken { get; set; } = Guid.NewGuid();

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public void UpdateUser(User newUser)
    {
        Username = newUser.Username;
        Password = newUser.Password;
        Roles = newUser.Roles;
    }

    public void GenerateRefreshToken()
    {
        RefreshToken = Guid.NewGuid();
    }
    
    public void addRole(Role role)
    {
        Roles.Add(role);
    }
}