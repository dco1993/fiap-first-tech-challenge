using ApiCadastro;
using Bogus;
using Domain.Entity;

namespace TestsApiCadastro
{
    public class TestsValidacoes
    {
        private readonly Faker _faker;

        public TestsValidacoes()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Validando se o nome vazio é válido")]
        [Trait("Validação", "Validando Contato")]
        public void ValidaContato_DeveRetornarErro_NomeVazio()
        {
            // Arrange         
            var novoContato = new Contato {
                NomeCompleto = "",
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };

            //act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            Assert.Equal("Nome inválido, de 5 a 100 caracteres são necessários. ", result);

        }

        [Fact(DisplayName = "Validando se o nome null é válido")]
        [Trait("Validação", "Validando Contato")]
        public void ValidaContato_DeveRetornarErro_NomeNull()
        {
            // Arrange         
            var novoContato = new Contato
            {
                NomeCompleto = null,
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };

            //act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            Assert.Equal("Nome inválido, de 5 a 100 caracteres são necessários. ", result);

        }

        [Fact(DisplayName = "Validando se o nome com menos de 5 caracteres é válido")]
        [Trait("Validação", "Validando Contato")]
        public void ValidaContato_DeveRetornarErro_NomeAbaixoMenorQue5()
        {
            // Arrange         
            var novoContato = new Contato
            {
                NomeCompleto = "Test",
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };

            //act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            Assert.Equal("Nome inválido, de 5 a 100 caracteres são necessários. ", result);

        }

        [Theory(DisplayName = "Validando se o e-mail incorreto é válido")]
        [Trait("Validação", "Validando Contato")]
        [InlineData("testetestecom")]
        [InlineData("teste.teste.com")]
        [InlineData("teste@testecom")]
        [InlineData("testeteste.com")]
        public void ValidaEmail_DeveRetornarErro_EmailInvalido(string email)
        {
            //Arrange
            var novoContato = new Contato
            {
                NomeCompleto = _faker.Person.FullName,
                Email = email,
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };

            //Act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            Assert.Equal("E-mail fornecido está inválido. ", result);
        }

        [Theory(DisplayName = "Validando se o telefone invalido é válido")]
        [Trait("Validação", "Validando Contato")]
        [InlineData("123a5678  ")]
        [InlineData("123a5678b  ")]
        [InlineData("a3e789sf")]
        public void ValidaTelefone_DeveRetornarErro_NumeroComLetras(string telefone)
        {
            //Arrange
            var novoContato = new Contato
            {
                NomeCompleto = _faker.Person.FullName,
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = telefone,
            };

            //Act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            Assert.Equal("Número de telefone inválido, apenas números são permitidos. ", result);
        }

        [Theory(DisplayName = "Validando se o telefone invalido é válido")]
        [Trait("Validação", "Validando Contato")]
        [InlineData("1234567")]
        [InlineData("123456")]
        [InlineData("12345")]
        [InlineData("1234567891")]
        public void ValidaTelefone_DeveRetornarErro_NumeroMenor(string telefone)
        {
            //Arrange
            var novoContato = new Contato
            {
                NomeCompleto = _faker.Person.FullName,
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = telefone,
            };

            //Act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            Assert.Equal("Número de telefone inválido, 8 ou 9 números são necessários. ", result);
        }

        [Fact(DisplayName = "Validando se o contato com todos os campos vazios é válido")]
        [Trait("Validação", "Validando Contato")]
        public void ValidaContato_DeveRetornarErro_TodosOsCamposVazios()
        {
            //Arrange
            var novoContato = new Contato
            {
                NomeCompleto = "",
                Email = "",
                TelefoneDdd = 0,
                TelefoneNum = "",
            };

            //Act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            string assertStr = @"E-mail fornecido está inválido. Nome inválido, de 5 a 100 caracteres são necessários. Número de telefone inválido, 8 ou 9 números são necessários. ";

            Assert.Equal(assertStr, result);
        }

        [Fact(DisplayName = "Validando se o contato com todos os campos preenchidos corretamente é válido")]
        [Trait("Validação", "Validando Contato")]
        public void ValidaContato_DeveRetornarSucesso_TodosOsCamposCorretos()
        {
            //Arrange
            var novoContato = new Contato
            {
                NomeCompleto = _faker.Person.FullName,
                Email = _faker.Person.Email,
                TelefoneDdd = 21,
                TelefoneNum = _faker.Random.Int(10000000, 999999999).ToString(),
            };

            //Act
            var result = Validacoes.ValidaContato(novoContato);

            //Assert
            Assert.Equal("", result);
        }
    }
}
