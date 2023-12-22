using Microsoft.AspNetCore.Identity;

namespace Models.Auth;

public class Usuario : IdentityUser
{
    public string? ProfileImageUrl {get; set;}

}