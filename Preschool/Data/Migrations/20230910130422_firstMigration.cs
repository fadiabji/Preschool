using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Childern");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassId",
                table: "Childern",
                newName: "IX_Childern_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Childern",
                table: "Childern",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Childern_Classes_ClassId",
                table: "Childern",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Childern_Classes_ClassId",
                table: "Childern");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Childern",
                table: "Childern");

            migrationBuilder.RenameTable(
                name: "Childern",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_Childern_ClassId",
                table: "Students",
                newName: "IX_Students_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
