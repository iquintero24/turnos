using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaTurnos.Web.Migrations
{
    /// <inheritdoc />
    public partial class MisegundaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Cajas_CajaId",
                table: "Turnos");

            migrationBuilder.AlterColumn<int>(
                name: "CajaId",
                table: "Turnos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinAtencion",
                table: "Turnos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Turnos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InicioAtencion",
                table: "Turnos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CajaId",
                table: "Atenciones",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "InicioAtencion",
                table: "Atenciones",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_FuncionarioId",
                table: "Turnos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Atenciones_CajaId",
                table: "Atenciones",
                column: "CajaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atenciones_Cajas_CajaId",
                table: "Atenciones",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Cajas_CajaId",
                table: "Turnos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Funcionarios_FuncionarioId",
                table: "Turnos",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atenciones_Cajas_CajaId",
                table: "Atenciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Cajas_CajaId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Funcionarios_FuncionarioId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_FuncionarioId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Atenciones_CajaId",
                table: "Atenciones");

            migrationBuilder.DropColumn(
                name: "FinAtencion",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "InicioAtencion",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "CajaId",
                table: "Atenciones");

            migrationBuilder.DropColumn(
                name: "InicioAtencion",
                table: "Atenciones");

            migrationBuilder.AlterColumn<int>(
                name: "CajaId",
                table: "Turnos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Cajas_CajaId",
                table: "Turnos",
                column: "CajaId",
                principalTable: "Cajas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
