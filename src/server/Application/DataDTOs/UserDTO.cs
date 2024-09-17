namespace Application.DataDTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public List<RoleDTO> Roles { get; set; }
    public string? Token { get; set; }
    public Guid? RefreshToken { get; set; }
}