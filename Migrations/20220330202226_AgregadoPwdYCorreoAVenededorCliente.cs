using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InduccionSemana4.Migrations
{
    public partial class AgregadoPwdYCorreoAVenededorCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pwd",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pwd",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "pwd",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "pwd",
                table: "Clientes");
        }
    }
}
