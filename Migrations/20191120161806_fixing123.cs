using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynu_projektas.Migrations
{
    public partial class fixing123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Services",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ArtistId",
                table: "Services",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ArtistId",
                table: "Services",
                column: "ArtistId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ArtistId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ArtistId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Services",
                nullable: false,
                defaultValue: 0);
        }
    }
}
