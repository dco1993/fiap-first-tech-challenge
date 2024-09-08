using ApiCadastro;
using Domain.Entity;
using Domain.Inputs;
using Domain.Repository;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

Console.WriteLine("Teste de automatizacao GitHub Actions + Docker Hub");

//Configuração dos grupos de endpoints
var regiaoGroup = app.MapGroup("/Regiao");
var contatoGroup = app.MapGroup("/Contato");

#region Endpoints Regiões

regiaoGroup.MapGet("/obterRegioes", (IRegiaoRepository regiao) => 
{
    try
    {
        return Results.Ok(regiao.ObterTodos());
    }
    catch(Exception e) 
    {
        return Results.BadRequest(e.Message);
    }
})
 .WithName("ObterRegioes")
 .WithTags("Regiao")
 .WithOpenApi();

#endregion

#region Endpoints Contatos

contatoGroup.MapGet("/obterTodosContatos", (IContatoRepository contato) =>
{
    try
    {
        return Results.Ok(contato.ObterTodos());
    }
    catch (Exception e) 
    {
        return Results.BadRequest(e.Message);
    }
})
 .WithName("ObterTodosContatos")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapGet("/obterContatoPorId", (IContatoRepository contatoRepo, int id) =>
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
    catch(Exception e)
    {
        return Results.BadRequest(e.Message);
    }
    
})
 .WithName("ObterContatoPorId")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapGet("/obterContatosPorDdd", (IContatoRepository contatoRepo, int ddd) =>
{
    try
    {
        return Results.Ok(contatoRepo.ObterContatosPorDdd(ddd));
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
    
})
 .WithName("ObterContatosPorDdd")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapPost("/cadastrarContato", (IContatoRepository contatoRepo, ContatoInput novoContato) => {

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

})
 .WithName("CadastrarContato")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapPut("/atualizarContato", (IContatoRepository contatoRepo, ContatoUpdate contatoAtualizado) => {

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

})
 .WithName("AtualizarContato")
 .WithTags("Contato")
 .WithOpenApi();

contatoGroup.MapDelete("/excluirContato", (IContatoRepository contato, int id) => {

    try
    {
        contato.Excluir(id);
        return Results.Ok("Contato excluido com sucesso.");
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }

})
 .WithName("ExcluirContato")
 .WithTags("Contato")
 .WithOpenApi();

#endregion

app.Run();



