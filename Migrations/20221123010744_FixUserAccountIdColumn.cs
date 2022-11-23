using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPIA.Migrations
{
    /// <inheritdoc />
    public partial class FixUserAccountIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_OwnerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_OwnerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "UserAccountId",
                table: "Tickets",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserAccountId",
                table: "Tickets",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_UserAccountId",
                table: "Tickets",
                column: "UserAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_UserAccountId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserAccountId",
                table: "Tickets");

            migrationBuilder.AlterColumn<long>(
                name: "UserAccountId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Tickets",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OwnerId",
                table: "Tickets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_OwnerId",
                table: "Tickets",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
