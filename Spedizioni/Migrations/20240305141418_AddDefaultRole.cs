using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spedizioni.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                    name: "Role",
                    table: "Users",
                    nullable: false,
                    defaultValue: "user"
                );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                    name: "Role",
                    table: "Users",
                    nullable: false
           );
        }
    }
}
