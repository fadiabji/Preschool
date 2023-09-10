using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    public partial class adddocumentsimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentsImage_Childern_ChildId",
                table: "DocumentsImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentsImage",
                table: "DocumentsImage");

            migrationBuilder.RenameTable(
                name: "DocumentsImage",
                newName: "DocumentsImages");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentsImage_ChildId",
                table: "DocumentsImages",
                newName: "IX_DocumentsImages_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentsImages",
                table: "DocumentsImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentsImages_Childern_ChildId",
                table: "DocumentsImages",
                column: "ChildId",
                principalTable: "Childern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentsImages_Childern_ChildId",
                table: "DocumentsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentsImages",
                table: "DocumentsImages");

            migrationBuilder.RenameTable(
                name: "DocumentsImages",
                newName: "DocumentsImage");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentsImages_ChildId",
                table: "DocumentsImage",
                newName: "IX_DocumentsImage_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentsImage",
                table: "DocumentsImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentsImage_Childern_ChildId",
                table: "DocumentsImage",
                column: "ChildId",
                principalTable: "Childern",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
