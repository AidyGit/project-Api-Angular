using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class Addmigration : Migration
    {
        /// <inheritdoc />

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RandomModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationId = table.Column<int>(type: "int", nullable: false),
                    WinningPurchaseId = table.Column<int>(type: "int", nullable: false),
                    RaffleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RandomModel_DonationsModel_DonationId",
                        column: x => x.DonationId,
                        principalTable: "DonationsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // חשוב: NoAction
                    table.ForeignKey(
                        name: "FK_RandomModel_PurchasesModel_WinningPurchaseId",
                        column: x => x.WinningPurchaseId,
                        principalTable: "PurchasesModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // חשוב: NoAction
                });

            migrationBuilder.CreateIndex(
                name: "IX_RandomModel_DonationId",
                table: "RandomModel",
                column: "DonationId");

            migrationBuilder.CreateIndex(
                name: "IX_RandomModel_WinningPurchaseId",
                table: "RandomModel",
                column: "WinningPurchaseId");
        }
    }
}
