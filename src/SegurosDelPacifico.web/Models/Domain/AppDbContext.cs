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

            builder.Entity<Empleado>().HasMany<Salario>( e=> e.Salarios)
               .WithOne().HasForeignKey(s=> s.EmpleadoId);

           // builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Empleado> Empleados {get; set;}
        public DbSet<Salario> Salarios {get; set;}


}