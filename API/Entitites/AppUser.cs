namespace API.Entitites;

public class AppUser
{
    public String Id { get; set; } = Guid.NewGuid().ToString();

    public required string DisplayName { get; set; }
    public required string Email { get; set; }
    
    
}
