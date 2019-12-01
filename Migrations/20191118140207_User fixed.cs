using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynu_projektas.Migrations
{
    public partial class Userfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Clients_ClientId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Artists_ArtistId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Artists_ArtistId",
                table: "Times");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Times",
                newName: "ArtistUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Times_ArtistId",
                table: "Times",
                newName: "IX_Times_ArtistUserId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Services",
                newName: "ArtistUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ArtistId",
                table: "Services",
                newName: "IX_Services_ArtistUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNr",
                table: "Users",
                type: "varchar(9)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Client_ClientId",
                table: "Registrations",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ArtistUserId",
                table: "Services",
                column: "ArtistUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Users_ArtistUserId",
                table: "Times",
                column: "ArtistUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Client_ClientId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ArtistUserId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Users_ArtistUserId",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNr",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameColumn(
                name: "ArtistUserId",
                table: "Times",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Times_ArtistUserId",
                table: "Times",
                newName: "IX_Times_ArtistId");

            migrationBuilder.RenameColumn(
                name: "ArtistUserId",
                table: "Services",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ArtistUserId",
                table: "Services",
                newName: "IX_Services_ArtistId");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ArtistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNr = table.Column<string>(type: "varchar(9)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ArtistId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Clients_ClientId",
                table: "Registrations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Artists_ArtistId",
                table: "Services",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Artists_ArtistId",
                table: "Times",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
