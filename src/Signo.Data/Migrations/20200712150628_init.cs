using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Signo.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Integrantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Unidade = table.Column<string>(type: "varchar(10)", nullable: false),
                    Matricula = table.Column<string>(type: "varchar(50)", nullable: true),
                    FuncaoBordo = table.Column<string>(type: "varchar(150)", nullable: false),
                    FuncaoContrato = table.Column<string>(type: "varchar(150)", nullable: false),
                    Empresa = table.Column<string>(type: "varchar(150)", nullable: false),
                    CepEndereco = table.Column<int>(type: "varchar(8)", nullable: true),
                    LogradouroEndereco = table.Column<string>(type: "varchar(350)", nullable: true),
                    BairroEndereco = table.Column<string>(type: "varchar(200)", nullable: true),
                    LocalidadeEndereco = table.Column<string>(type: "varchar(200)", nullable: true),
                    UfEndereco = table.Column<string>(type: "varchar(50)", nullable: true),
                    NumeroEndereco = table.Column<string>(type: "varchar(150)", nullable: true),
                    ComplementoEndereco = table.Column<string>(type: "varchar(350)", nullable: true),
                    Telefone = table.Column<string>(type: "varchar(150)", nullable: true),
                    ImgFoto = table.Column<string>(type: "varchar(300)", nullable: false),
                    ImgSign = table.Column<string>(type: "varchar(300)", nullable: false),
                    Nacionalidade = table.Column<string>(type: "varchar(150)", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Admissao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DoB = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrantes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Integrantes");
        }
    }
}
