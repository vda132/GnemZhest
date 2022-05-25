using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLayer.Migrations
{
    public partial class cartdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Good",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    Image1 = table.Column<string>(type: "varchar(max)", nullable: false),
                    Image2 = table.Column<string>(type: "varchar(max)", nullable: false),
                    Image3 = table.Column<string>(type: "varchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    IsAvaliable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Good_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "varchar(max)", nullable: false),
                    Login = table.Column<string>(type: "varchar(max)", nullable: false),
                    Password = table.Column<string>(type: "varchar(max)", nullable: false),
                    Email = table.Column<string>(type: "varchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(max)", nullable: false),
                    Role = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GoodId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "varchar(max)", nullable: true),
                    OrderAdress = table.Column<string>(type: "varchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_PK", x => x.Id);
                    table.ForeignKey(
                        name: "Order_GoodId_FK",
                        column: x => x.GoodId,
                        principalTable: "Good",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Order_User_FK",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_GoodId",
                table: "Order",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Good");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
