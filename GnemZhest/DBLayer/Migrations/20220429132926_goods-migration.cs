using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLayer.Migrations
{
    public partial class goodsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Good",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Good",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Good",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Good");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Good");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Good");

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodId = table.Column<int>(type: "int", nullable: true),
                    PhotoPath = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Photo_PK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Good_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Good",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_GoodId",
                table: "Photo",
                column: "GoodId");
        }
    }
}
