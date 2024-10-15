using ApiCadastro;
using Domain.Entity;
using Domain.Inputs;
using Domain.Repository;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Prometheus;
using Prometheus.SystemMetrics;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>();

//Configuração para ignorar os ciclos infinitos durante a serialização de objetos
builder.Services.Configure<JsonOptions>(options =>
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Configurar endpoint de métricas
var port = builder.Environment.IsDevelopment() ? 0 : 2051;
builder.Services.AddMetricServer(options =>
{
    options.Port = Convert.ToUInt16(port);
});

//Obter métricas do sistema
builder.Services.AddSystemMetrics();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
}

//Obter métricas das solicitações HTTP
app.UseHttpMetrics();

//Configuração dos grupos de endpoints
var regiaoGroup = app.MapGroup("/Regiao");
var contatoGroup = app.MapGroup("/Contato");

#region Configuração Métricas

Metrics.SuppressDefaultMetrics(new SuppressDefaultMetricOptions
{
    SuppressEventCounters = true
});

//Regiões
Gauge obterRegioesDuracao = Metrics.CreateGauge("duration_obter_regioes", "Duração em milisegundos da chamada para obter regiões.");

//Contatos
Gauge obterContatosDuracao = Metrics.CreateGauge("duration_obter_contatos", "Duração em milisegundos da chamada para obter contatos.");
Gauge obterContatosIdDuracao = Metrics.CreateGauge("duration_obter_contatos_id", "Duração em milisegundos da chamada para obter contatos por ID.");
Gauge obterContatosDddDuracao = Metrics.CreateGauge("duration_obter_contatos_ddd", "Duração em milisegundos da chamada para obter contatos por ID por DDD.");
Gauge cadastrarContatoDuracao = Metrics.CreateGauge("duration_cadastrar_contato", "Duração em milisegundos da chamada para cadastrar um contato.");
Gauge atualizarContatoDuracao = Metrics.CreateGauge("duration_atualizar_contato", "Duração em milisegundos da chamada para atualizar um contato.");
Gauge excluirContatoDuracao = Metrics.CreateGauge("duration_excluir_contato", "Duração em milisegundos da chamada para excluir um contato.");

#endregion

#region Endpoints Regiões

regiaoGroup.MapGet("/obterRegioes", (IRegiaoRepository regiao) =>
{
    using (obterRegioesDuracao.NewTimer())
    {
        try
        {
            return Results.Ok(regiao.ObterTodos());
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
})
 .WithName("ObterRegioes")
 .WithTags("Regiao")
 .WithOpenApi();

#endregion

#region Endpoints Contatos

contatoGroup.MapGet("/obterTodosContatos", (IContatoRepository contato) =>
{
    using (obterContatosDuracao.NewTimer())
    {
        try
        {
            return Results.Ok(contato.ObterTodos());
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
})
 .WithName("ObterTodosContatos")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapGet("/obterContatoPorId", (IContatoRepository contatoRepo, int id) =>
{
    using (obterContatosIdDuracao.NewTimer())
    {
        try
        {
            var contato = contatoRepo.ObterPorId(id);
            if (contato is not null)
            {
                return Results.Ok(contato);
            }
            else
            {
                return Results.BadRequest("Nenhum contato com esse ID foi encontrado.");
            }
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
})
 .WithName("ObterContatoPorId")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapGet("/obterContatosPorDdd", (IContatoRepository contatoRepo, int ddd) =>
{
    using (obterContatosDddDuracao.NewTimer())
    {
        try
        {
            if (ddd == 0) return Results.BadRequest("O campo DDD precisa ser preenchido.");

            IList<Contato> lstContato = contatoRepo.ObterContatosPorDdd(ddd);

            if (lstContato.Count == 0) return Results.BadRequest("Sua pesquisa não retornou resultados.");

            return Results.Ok(lstContato);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
})
 .WithName("ObterContatosPorDdd")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapPost("/cadastrarContato", (IContatoRepository contatoRepo, ContatoInput novoContato) =>
{
    using (cadastrarContatoDuracao.NewTimer())
    {
        var contato = new Contato
        {
            NomeCompleto = novoContato.NomeCompleto,
            TelefoneDdd = novoContato.TelefoneDdd,
            TelefoneNum = novoContato.TelefoneNum,
            Email = novoContato.Email
        };

        var validacao = Validacoes.ValidaContato(contato);

        if (validacao.IsNullOrEmpty())
        {
            try
            {
                contatoRepo.Cadastrar(contato);
                return Results.Ok("Contato cadastrado com sucesso.");
            }
            catch (DbUpdateException e)
            {
                return Results.BadRequest("O código DDD informado é inválido.");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
        else
        {
            return Results.BadRequest(validacao);
        }
    }
})
 .WithName("CadastrarContato")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapPut("/atualizarContato", (IContatoRepository contatoRepo, ContatoUpdate contatoAtualizado) =>
{
    using (atualizarContatoDuracao.NewTimer())
    {
        var contato = new Contato
        {
            Id = contatoAtualizado.Id,
            NomeCompleto = contatoAtualizado.NomeCompleto,
            TelefoneDdd = contatoAtualizado.TelefoneDdd,
            TelefoneNum = contatoAtualizado.TelefoneNum,
            Email = contatoAtualizado.Email
        };

        var validacao = Validacoes.ValidaContato(contato);

        if (validacao.IsNullOrEmpty())
        {
            try
            {
                contatoRepo.Atualizar(contato);
                return Results.Ok("Contato atualizado com sucesso.");
            }
            catch (DbUpdateException e)
            {
                return Results.BadRequest("O código DDD informado é inválido.");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
        else
        {
            return Results.BadRequest(validacao);
        }
    }
})
 .WithName("AtualizarContato")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapDelete("/excluirContato", (IContatoRepository contato, int id) =>
{
    using (excluirContatoDuracao.NewTimer())
    {
        try
        {
            if (id == 0) return Results.BadRequest("O campo ID precisa ser preenchido.");
            contato.Excluir(id);
            return Results.Ok("Contato excluido com sucesso.");
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
})
 .WithName("ExcluirContato")
 .WithTags("Contato")
 .WithOpenApi();

#endregion

app.Run();

public partial class Program { }