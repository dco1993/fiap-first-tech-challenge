using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Bogus;

namespace TestsApiCadastro
{
    public class TestsIntegracao : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Faker _faker;

        public TestsIntegracao(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _faker = new Faker();
        }

        [Fact(DisplayName = "Teste de integração no endpoint para obter todos os contatos.")]
        [Trait("Integração", "Obter Contatos")]
        public async Task Get_ObterTodosContatos_DeveRetornarSucesso()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Contato/obterTodosContatos");

            // Assert
            var statusCode = response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
        }

        [Fact(DisplayName = "Teste de integração para tentativa de cadastro de usuário com informações corretas.")]
        [Trait("Integração", "Cadastrar Contato")]
        public async Task Post_CadastrarContatoValido_DeveRetornarSucesso()
        {
            // Arrange
            var client = _factory.CreateClient();
            var novoContato = new Contato
            {
                NomeCompleto = "TI_"+_faker.Person.FullName,
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };
            var content = new StringContent(JsonConvert.SerializeObject(novoContato), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/Contato/cadastrarContato", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Contato cadastrado com sucesso.", responseString.Trim('"'));
        }

        [Fact(DisplayName = "Teste de integração para tentativa de cadastro de usuário com nome menor que o necessário.")]
        [Trait("Integração", "Cadastrar Contato")]
        public async Task Post_CadastrarContatoNomeErrado_DeveRetornarErro()
        {
            // Arrange
            var client = _factory.CreateClient();
            var novoContato = new Contato
            {
                NomeCompleto = "TI_",
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };
            var content = new StringContent(JsonConvert.SerializeObject(novoContato), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/Contato/cadastrarContato", content);

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Nome inválido, de 5 a 100 caracteres são necessários. ", responseString.Trim('"'));
        }

        [Fact(DisplayName = "Teste de integração para atualizar o usuário de ID 21, todas as informações estão corretas.")]
        [Trait("Integração", "Atualizar Contato")]
        public async Task Put_AtualizarContatoCorreto_DeveRetornarSucesso()
        {
            // Arrange
            var client = _factory.CreateClient();
            var dt = DateTime.Now;
            var novoContato = new Contato
            {
                Id = 21,
                NomeCompleto = string.Join("_", "TI_Daniel Cintra_", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second),
                Email = "video@apresentacao.com.br",
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };
            var content = new StringContent(JsonConvert.SerializeObject(novoContato), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("/Contato/atualizarContato", content);

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Contato atualizado com sucesso.", responseString.Trim('"'));
        }

        [Fact(DisplayName = "Teste de integração para tentativa de excluir um contato sem enviar um ID.")]
        [Trait("Integração", "Excluir Contato")]
        public async Task Delete_ExcluirUsuarioSemId_DeveRetornarErro()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/Contato/excluirContato?id=0");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("O campo ID precisa ser preenchido.", responseString.Trim('"'));
        }
    }
}
