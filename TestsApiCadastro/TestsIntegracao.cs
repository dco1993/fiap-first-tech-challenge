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

        [Fact]
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
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Contato cadastrado com sucesso.", responseString.Trim('"'));
        }
    }
}
