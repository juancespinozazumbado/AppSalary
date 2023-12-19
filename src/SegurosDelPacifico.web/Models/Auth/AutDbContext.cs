using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models.Auth;

public class AuthContext : IdentityDbContext<Usuario>
{
    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {


        builder.Entity<Usuario>().HasData( CreateAdminUser());

        base.OnModelCreating(builder);
       
       
    }

    private Usuario CreateAdminUser(){

        var admin = new Usuario(){
            UserName = "Admin",
            NormalizedUserName = "ADMIN"
        };

        var hassher = new PasswordHasher<Usuario>();
        admin.PasswordHash = hassher.HashPassword(admin, "seguros2023");

        return admin;

    }
}
