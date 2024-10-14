using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace TestsApiCadastro
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("../ApiCadastro/appsettings.json").Build();

            builder.ConfigureServices(services =>
            {
                /*var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDbContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);*/

                // Create open SqliteConnection so EF won't automatically close it.
                /*services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<ApplicationDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });*/
                services.AddEndpointsApiExplorer();
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                }, ServiceLifetime.Scoped);

                services.AddScoped<IContatoRepository, ContatoRepository>();
                services.AddScoped<IRegiaoRepository, RegiaoRepository>();
            });

            builder.UseEnvironment("Development");
        }
    }
}
