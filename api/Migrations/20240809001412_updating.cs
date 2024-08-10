using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class updating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17a887f3-2b91-4c9b-bd88-863c5f0234c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c27bb87-6afe-42b6-b96f-5efe56fc9ca1");

            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ff19d15-5199-49ee-8a53-a456db47bb86", null, "Admin", "ADMIN" },
                    { "a700af82-a0ec-4392-bc21-5544d572d963", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ff19d15-5199-49ee-8a53-a456db47bb86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a700af82-a0ec-4392-bc21-5544d572d963");

            migrationBuilder.DropColumn(
                name: "fullName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17a887f3-2b91-4c9b-bd88-863c5f0234c2", null, "User", "USER" },
                    { "7c27bb87-6afe-42b6-b96f-5efe56fc9ca1", null, "Admin", "ADMIN" }
                });
        }
    }
}
