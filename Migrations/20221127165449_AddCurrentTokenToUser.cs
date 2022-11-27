using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPIA.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentTokenToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentToken",
                table: "AspNetUsers");
        }
    }
}
