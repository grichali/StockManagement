using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba390bad-0377-4238-958b-f412bb1702f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5276546-7028-4cf1-9b5c-caf94ddda183");

            migrationBuilder.AddColumn<float>(
                name: "BuyPrice",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8d83a375-55d7-4ec1-ad56-d4f380426df1", null, "User", "USER" },
                    { "b7b8eb72-d6d9-442c-9906-e078e29efe73", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d83a375-55d7-4ec1-ad56-d4f380426df1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7b8eb72-d6d9-442c-9906-e078e29efe73");

            migrationBuilder.DropColumn(
                name: "BuyPrice",
                table: "Product");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ba390bad-0377-4238-958b-f412bb1702f1", null, "User", "USER" },
                    { "d5276546-7028-4cf1-9b5c-caf94ddda183", null, "Admin", "ADMIN" }
                });
        }
    }
}
