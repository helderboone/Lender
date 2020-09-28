using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lender.API.Migrations
{
    public partial class AddPKLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Loans",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_FriendId",
                table: "Loans",
                column: "FriendId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_FriendId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Loans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                columns: new[] { "FriendId", "GameId" });
        }
    }
}
