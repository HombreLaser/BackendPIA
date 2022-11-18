using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPIA.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenExpiryTimeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "24edc3d6-bf9c-41a1-9371-224e4419ccb0", "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24edc3d6-bf9c-41a1-9371-224e4419ccb0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec", "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SessionToken", "SessionTokenExpiryTime", "TwoFactorEnabled", "UserName" },
                values: new object[] { "24edc3d6-bf9c-41a1-9371-224e4419ccb0", 0, "bd624bcb-3f06-4bce-b924-2666f82e5f23", "admin@example.com", false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEL19rXYOEkR3ftL+T5E5vlsLGPu3HSnJuTSLNp/nyffvQvaXlNJFqU1UO3VKB+K6yg==", null, false, "282566ca-8a3b-4310-8e61-8380d16fa07e", null, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec", "24edc3d6-bf9c-41a1-9371-224e4419ccb0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec", "24edc3d6-bf9c-41a1-9371-224e4419ccb0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "24edc3d6-bf9c-41a1-9371-224e4419ccb0");

            migrationBuilder.DropColumn(
                name: "SessionTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24edc3d6-bf9c-41a1-9371-224e4419ccb0", "24edc3d6-bf9c-41a1-9371-224e4419ccb0", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SessionToken", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec", 0, "8bbb8fce-308b-4822-97e1-5741fc955a90", "admin@example.com", false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEENVY01/0BOrBai8zaioq9GOr+ftYIZhUBtulPtda1tTREUCOeVst9cnrB7Ogz4Bsg==", null, false, "5a1e1053-690e-4610-ab66-7a86fe2e04c8", null, false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "24edc3d6-bf9c-41a1-9371-224e4419ccb0", "d42006bc-7f69-4aa4-b247-eb9e2abfe0ec" });
        }
    }
}
