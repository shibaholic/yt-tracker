namespace service_infrastructure.entities;

public class UserAccount
{
    public Guid id { get; set; }
    public string username { get; set; }
    public string password_hash { get; set; }
}