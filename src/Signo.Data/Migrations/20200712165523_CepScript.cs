using Microsoft.EntityFrameworkCore.Migrations;

namespace Signo.Data.Migrations
{
    public partial class CepScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CepEndereco",
                table: "Integrantes",
                type: "varchar(8)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CepEndereco",
                table: "Integrantes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldNullable: true);
        }
    }
}
