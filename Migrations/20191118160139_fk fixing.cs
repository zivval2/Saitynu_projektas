using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynu_projektas.Migrations
{
    public partial class fkfixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ArtistUserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ArtistUserId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "ArtistUserId",
                table: "Services",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Services",
                newName: "ArtistUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ArtistUserId",
                table: "Services",
                column: "ArtistUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ArtistUserId",
                table: "Services",
                column: "ArtistUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
