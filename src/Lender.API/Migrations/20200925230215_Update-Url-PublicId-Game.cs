using Microsoft.EntityFrameworkCore.Migrations;

namespace Lender.API.Migrations
{
    public partial class UpdateUrlPublicIdGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Games",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Games",
                newName: "PhotoPublicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Games",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "PhotoPublicId",
                table: "Games",
                newName: "PublicId");
        }
    }
}
