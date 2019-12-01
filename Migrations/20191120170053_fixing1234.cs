using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynu_projektas.Migrations
{
    public partial class fixing1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ArtistId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Services",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ArtistId",
                table: "Services",
                newName: "IX_Services_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_UserId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Services",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_UserId",
                table: "Services",
                newName: "IX_Services_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ArtistId",
                table: "Services",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
