using Microsoft.EntityFrameworkCore.Migrations;

namespace Lender.API.Migrations
{
    public partial class UpdateUrlPublicIdFriend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Friends",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Friends",
                newName: "PhotoPublicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Friends",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "PhotoPublicId",
                table: "Friends",
                newName: "PublicId");
        }
    }
}
