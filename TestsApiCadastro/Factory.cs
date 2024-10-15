using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Prometheus;

namespace TestsApiCadastro
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("../ApiCadastro/appsettings.json").Build();

            builder.ConfigureServices(services =>
            {
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
