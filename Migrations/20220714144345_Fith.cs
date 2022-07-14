using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Disney.Migrations
{
    public partial class Fith : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Usersw_Email",
                table: "Usersw");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usersw",
                table: "Usersw");

            migrationBuilder.RenameTable(
                name: "Usersw",
                newName: "Users");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Email",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usersw");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Usersw_Email",
                table: "Usersw",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usersw",
                table: "Usersw",
                column: "Id");
        }
    }
}
