using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMITCOMIDAS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AjustesPersonaYPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPedidosMes",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PedidosRestantesMes",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaActualizacionPedidos",
                table: "Personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "DescontadoDeCuota",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRecepcion",
                table: "Pedidos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivoDevolucion",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPedidosMes",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "PedidosRestantesMes",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "UltimaActualizacionPedidos",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "DescontadoDeCuota",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "FechaRecepcion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "MotivoDevolucion",
                table: "Pedidos");
        }
    }
}
