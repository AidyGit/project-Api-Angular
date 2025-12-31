using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonorsModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorsModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonationsModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PriceTiket = table.Column<int>(type: "int", nullable: false),
                    DonorsId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationsModel_DonorsModel_DonorsId",
                        column: x => x.DonorsId,
                        principalTable: "DonorsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartModel_UserModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiftShoppingCartModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DonationId = table.Column<int>(type: "int", nullable: false),
                    DonationsId = table.Column<int>(type: "int", nullable: true),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftShoppingCartModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftShoppingCartModel_DonationsModel_DonationsId",
                        column: x => x.DonationsId,
                        principalTable: "DonationsModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiftShoppingCartModel_ShoppingCartModel_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCartModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasesModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationId = table.Column<int>(type: "int", nullable: false),
                    DonationsId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasesModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasesModel_DonationsModel_DonationsId",
                        column: x => x.DonationsId,
                        principalTable: "DonationsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasesModel_ShoppingCartModel_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCartModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasesModel_UserModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RandonModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationId = table.Column<int>(type: "int", nullable: false),
                    DonationsId = table.Column<int>(type: "int", nullable: true),
                    WinningPurchaseId = table.Column<int>(type: "int", nullable: false),
                    RaffleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandonModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RandonModel_DonationsModel_DonationsId",
                        column: x => x.DonationsId,
                        principalTable: "DonationsModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RandonModel_PurchasesModel_WinningPurchaseId",
                        column: x => x.WinningPurchaseId,
                        principalTable: "PurchasesModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationsModel_DonorsId",
                table: "DonationsModel",
                column: "DonorsId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftShoppingCartModel_DonationsId",
                table: "GiftShoppingCartModel",
                column: "DonationsId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftShoppingCartModel_ShoppingCartId",
                table: "GiftShoppingCartModel",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesModel_DonationsId",
                table: "PurchasesModel",
                column: "DonationsId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesModel_ShoppingCartId",
                table: "PurchasesModel",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesModel_UserId",
                table: "PurchasesModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RandonModel_DonationsId",
                table: "RandonModel",
                column: "DonationsId");

            migrationBuilder.CreateIndex(
                name: "IX_RandonModel_WinningPurchaseId",
                table: "RandonModel",
                column: "WinningPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartModel_UserId",
                table: "ShoppingCartModel",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftShoppingCartModel");

            migrationBuilder.DropTable(
                name: "RandonModel");

            migrationBuilder.DropTable(
                name: "PurchasesModel");

            migrationBuilder.DropTable(
                name: "DonationsModel");

            migrationBuilder.DropTable(
                name: "ShoppingCartModel");

            migrationBuilder.DropTable(
                name: "DonorsModel");

            migrationBuilder.DropTable(
                name: "UserModel");
        }
    }
}
