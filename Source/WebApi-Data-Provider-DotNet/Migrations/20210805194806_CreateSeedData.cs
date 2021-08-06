using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiDataProviderDotNet.Migrations
{
    public partial class CreateSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "ValueOne", "ValueTwo" },
                values: new object[] { "4c56f180-c0db-4954-bae6-4f156b8f17cf", "wossewodda-3728@yopmail.com", "Valeur test 1", "Seconde valeur test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4c56f180-c0db-4954-bae6-4f156b8f17cf");
        }
    }
}
