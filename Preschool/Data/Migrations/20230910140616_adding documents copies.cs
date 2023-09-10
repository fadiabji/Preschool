using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    public partial class addingdocumentscopies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Childern");

            migrationBuilder.CreateTable(
                name: "DocumentsImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsImage_Childern_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Childern",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsImage_ChildId",
                table: "DocumentsImage",
                column: "ChildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentsImage");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Childern",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
