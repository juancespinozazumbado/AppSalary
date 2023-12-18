using Microsoft.EntityFrameworkCore;

namespace Models.Domain;

public class AppDbContext : DbContext
{

      public AppDbContext (DbContextOptions<AppDbContext> opciones) 
        : base(opciones)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


}