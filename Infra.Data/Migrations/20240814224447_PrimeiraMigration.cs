using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "REGIAO",
                columns: table => new
                {
                    CD_REGIAO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DDD_REGIAO = table.Column<int>(type: "INT", nullable: false),
                    NOME_REGIAO = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGIAO", x => x.CD_REGIAO);
                    table.UniqueConstraint("AK_REGIAO_DDD_REGIAO", x => x.DDD_REGIAO);
                });

            migrationBuilder.CreateTable(
                name: "CONTATO",
                columns: table => new
                {
                    CD_CONTATO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_COMPLETO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TELEFONE_DDD = table.Column<int>(type: "INT", maxLength: 2, nullable: false),
                    TELEFONE_NUMERO = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTATO", x => x.CD_CONTATO);
                    table.ForeignKey(
                        name: "FK_CONTATO_REGIAO_TELEFONE_DDD",
                        column: x => x.TELEFONE_DDD,
                        principalTable: "REGIAO",
                        principalColumn: "DDD_REGIAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTATO_TELEFONE_DDD",
                table: "CONTATO",
                column: "TELEFONE_DDD");

            //Carga inicial de dados da tabela REGIAO
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (11, 'São Paulo - Região Metropolitana')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (12, 'São Paulo - Vale do Paraíba e Litoral Norte')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (13, 'São Paulo - Baixada Santista e Vale do Ribeira')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (14, 'São Paulo - Bauru e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (15, 'São Paulo - Sorocaba e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (16, 'São Paulo - Ribeirão Preto e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (17, 'São Paulo - São José do Rio Preto e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (18, 'São Paulo - Presidente Prudente e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (19, 'São Paulo - Campinas e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (21, 'Rio de Janeiro - Região Metropolitana')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (22, 'Rio de Janeiro - Norte Fluminense')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (24, 'Rio de Janeiro - Sul Fluminense')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (27, 'Espírito Santo - Vitória e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (28, 'Espírito Santo - Sul Capixaba')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (31, 'Minas Gerais - Belo Horizonte e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (32, 'Minas Gerais - Zona da Mata')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (33, 'Minas Gerais - Vale do Rio Doce')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (34, 'Minas Gerais - Triângulo Mineiro')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (35, 'Minas Gerais - Sul de Minas')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (37, 'Minas Gerais - Centro-Oeste')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (38, 'Minas Gerais - Norte de Minas')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (41, 'Paraná - Curitiba e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (42, 'Paraná - Ponta Grossa e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (43, 'Paraná - Londrina e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (44, 'Paraná - Maringá e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (45, 'Paraná - Cascavel e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (46, 'Paraná - Pato Branco e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (47, 'Santa Catarina - Joinville e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (48, 'Santa Catarina - Florianópolis e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (49, 'Santa Catarina - Chapecó e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (51, 'Rio Grande do Sul - Porto Alegre e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (53, 'Rio Grande do Sul - Pelotas e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (54, 'Rio Grande do Sul - Caxias do Sul e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (55, 'Rio Grande do Sul - Santa Maria e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (61, 'Distrito Federal - Brasília')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (62, 'Goiás - Goiânia e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (63, 'Tocantins - Palmas e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (64, 'Goiás - Sul Goiano')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (65, 'Mato Grosso - Cuiabá e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (66, 'Mato Grosso - Norte Mato-Grossense')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (67, 'Mato Grosso do Sul - Campo Grande e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (68, 'Acre - Rio Branco e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (69, 'Rondônia - Porto Velho e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (71, 'Bahia - Salvador e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (73, 'Bahia - Sul Baiano')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (74, 'Bahia - Norte Baiano')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (75, 'Bahia - Recôncavo')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (77, 'Bahia - Oeste Baiano')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (79, 'Sergipe - Aracaju e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (81, 'Pernambuco - Recife e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (82, 'Alagoas - Maceió e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (83, 'Paraíba - João Pessoa e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (84, 'Rio Grande do Norte - Natal e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (85, 'Ceará - Fortaleza e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (86, 'Piauí - Teresina e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (87, 'Pernambuco - Interior Pernambucano')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (88, 'Ceará - Interior Cearense')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (89, 'Piauí - Interior Piauiense')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (91, 'Pará - Belém e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (92, 'Amazonas - Manaus e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (93, 'Pará - Oeste Paraense')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (94, 'Pará - Sudeste Paraense')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (95, 'Roraima - Boa Vista e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (96, 'Amapá - Macapá e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (97, 'Amazonas - Interior do Amazonas')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (98, 'Maranhão - São Luís e Região')");
            migrationBuilder.Sql("INSERT INTO REGIAO (DDD_REGIAO, NOME_REGIAO) VALUES (99, 'Maranhão - Interior do Maranhão')");

            //Carga inicial de dados da tabela CONTATO
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Joana Teste', 'joana@teste.com', 21, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Robson Teste', 'robson@teste.com', 21, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Marcela Teste', 'marcela@teste.com', 21, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Erick Teste', 'erick@teste.com', 21, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('João Teste', 'joao@teste.com', 21, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Luana Teste', 'Luana@teste.com', 21, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Maria Teste', 'maria@teste.com', 11, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Carla Teste', 'carla@teste.com', 11, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Patrícia Teste', 'patricia@teste.com', 11, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Pedro Teste', 'pedro@teste.com', 11, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Thiago Teste', 'thiago@teste.com', 11, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Luiz Teste', 'luiz@teste.com', 11, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Tânia Teste', 'tania@teste.com', 31, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Luiza Teste', 'luiza@teste.com', 31, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Carlos Teste', 'carlos@teste.com', 31, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Henrique Teste', 'henrique@teste.com', 31, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Victor Teste', 'victor@teste.com', 31, '123456789')");
            migrationBuilder.Sql("INSERT INTO CONTATO(NOME_COMPLETO, EMAIL, TELEFONE_DDD, TELEFONE_NUMERO) VALUES ('Felipe Teste', 'felipe@teste.com', 31, '123456789')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONTATO");

            migrationBuilder.DropTable(
                name: "REGIAO");
        }
    }
}
