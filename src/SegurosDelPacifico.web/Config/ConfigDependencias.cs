using Microsoft.EntityFrameworkCore;
using Models.Auth;
using Models.Domain;

namespace Config;

public static class ConfigDependencias
{

    public static void ConfigureDatabases(IServiceCollection services, IConfiguration configuration,
       bool useMemroyDb = true )
    {
       if(useMemroyDb){

         var AppConnectionString = configuration.GetSection("SQLite-config")["App"];
         var identityConnectionString = configuration.GetSection("SQLite-config")["Auth"];

        services.AddDbContext<AppDbContext>( options => options.UseSqlite(AppConnectionString));
        services.AddDbContext<AuthContext>( options => options.UseSqlite(identityConnectionString));

       }
        
    }
}