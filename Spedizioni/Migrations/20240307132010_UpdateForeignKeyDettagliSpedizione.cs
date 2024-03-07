using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spedizioni.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyDettagliSpedizione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DettagliSpedizione_IdSpedizione",
                table: "DettagliSpedizione",
                column: "IdSpedizione");

            migrationBuilder.AddForeignKey(
                name: "FK_DettagliSpedizione_Spedizione_IdSpedizione",
                table: "DettagliSpedizione",
                column: "IdSpedizione",
                principalTable: "Spedizione",
                principalColumn: "IdSpedizione",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DettagliSpedizione_Spedizione_IdSpedizione",
                table: "DettagliSpedizione");

            migrationBuilder.DropIndex(
                name: "IX_DettagliSpedizione_IdSpedizione",
                table: "DettagliSpedizione");
        }
    }
}
