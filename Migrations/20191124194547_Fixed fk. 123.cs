using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynu_projektas.Migrations
{
    public partial class Fixedfk123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Users_ClientUserId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Services_ServiceId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Times_TimeId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Users_ArtistUserId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_ArtistUserId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_ClientUserId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_ServiceId",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_TimeId",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "ArtistUserId",
                table: "Times",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Services",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "ClientUserId",
                table: "Registrations",
                newName: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Times",
                newName: "ArtistUserId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Services",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Registrations",
                newName: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_ArtistUserId",
                table: "Times",
                column: "ArtistUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ClientUserId",
                table: "Registrations",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ServiceId",
                table: "Registrations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_TimeId",
                table: "Registrations",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Users_ClientUserId",
                table: "Registrations",
                column: "ClientUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Services_ServiceId",
                table: "Registrations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Times_TimeId",
                table: "Registrations",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "TimeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Users_ArtistUserId",
                table: "Times",
                column: "ArtistUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
