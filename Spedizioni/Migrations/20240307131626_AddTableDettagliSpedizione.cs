using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spedizioni.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDettagliSpedizione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DettagliSpedizione",
                columns: table => new
                {
                    IdDettagliSpedizione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSpedizione = table.Column<int>(type: "int", nullable: false),
                    Stato = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DettagliSpedizione", x => x.IdDettagliSpedizione);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DettagliSpedizione");
        }
    }
}
