using System.Data.Common;
using GdprConsentApp.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.MySql;
using Xunit;

namespace GdprConsentApp.Tests.Integration;

public class GdprConsentAppFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MySqlContainer _dbContainer = new MySqlBuilder()
        .WithDatabase("db")
        .WithUsername("user")
        .WithPassword("password")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<MysqlDbContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            
            services.AddScoped<MysqlDbContext>();
            services.AddDbContext<MysqlDbContext>(options =>
            {
                var connectionString = _dbContainer.GetConnectionString();
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            
            services.AddScoped<DatabaseInitializer>();
            
        });

    }

    // private void SeedDb(IWebHostBuilder builder)
    // {
    //     using var scope = builder.Co();
    //
    //     MysqlDbContext db = scope.ServiceProvider.GetRequiredService<MysqlDbContext>();
    //
    // var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>(db);
    //
    // databaseInitializer.InitializeDb();
    // } 

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        
        // var databaseInitializer = new DatabaseInitializer(_dbContainer.);
        //
        // databaseInitializer.InitializeDb();
        
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}
