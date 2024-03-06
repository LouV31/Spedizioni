using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spedizioni.Migrations
{
    /// <inheritdoc />
    public partial class AddSpedizioneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Spedizione",
                columns: table => new
                {
                    IdSpedizione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    DataSpedizione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Destinazione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NominativoDestinatario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoSpedizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataConsegna = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spedizione", x => x.IdSpedizione);
                    table.ForeignKey(
                        name: "FK_Spedizione_Clienti_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spedizione_IdCliente",
                table: "Spedizione",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spedizione");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
