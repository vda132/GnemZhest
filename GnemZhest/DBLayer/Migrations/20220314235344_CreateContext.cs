using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLayer.Migrations
{
    public partial class CreateContext : Migration
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
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath = table.Column<string>(type: "varchar(max)", nullable: false),
                    GoodId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    GoodId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsOrdered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Cart_PK", x => x.Id);
                    table.ForeignKey(
                        name: "Cart_GoodId_FK",
                        column: x => x.GoodId,
                        principalTable: "Good",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Cart_UserId_FK",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderAdress = table.Column<string>(type: "varchar(max)", nullable: false),
                    IsSend = table.Column<bool>(type: "bit", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_PK", x => x.Id);
                    table.ForeignKey(
                        name: "Order_CartId_FK",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_GoodId",
                table: "Cart",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId",
                table: "Order",
                column: "CartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_GoodId",
                table: "Photo",
                column: "GoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Good");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
