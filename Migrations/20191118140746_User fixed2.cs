using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saitynu_projektas.Migrations
{
    public partial class Userfixed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Client_ClientId",
                table: "Registrations");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Registrations",
                newName: "ClientUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_ClientId",
                table: "Registrations",
                newName: "IX_Registrations_ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Users_ClientUserId",
                table: "Registrations",
                column: "ClientUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Users_ClientUserId",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "ClientUserId",
                table: "Registrations",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_ClientUserId",
                table: "Registrations",
                newName: "IX_Registrations_ClientId");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNr = table.Column<string>(type: "varchar(9)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Client_ClientId",
                table: "Registrations",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
