using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class FixDonationIdNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesModel_DonationsModel_DonationsId",
                table: "PurchasesModel");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesModel_DonationsId",
                table: "PurchasesModel");

            migrationBuilder.DropColumn(
                name: "DonationsId",
                table: "PurchasesModel");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesModel_DonationId",
                table: "PurchasesModel",
                column: "DonationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesModel_DonationsModel_DonationId",
                table: "PurchasesModel",
                column: "DonationId",
                principalTable: "DonationsModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesModel_DonationsModel_DonationId",
                table: "PurchasesModel");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesModel_DonationId",
                table: "PurchasesModel");

            migrationBuilder.AddColumn<int>(
                name: "DonationsId",
                table: "PurchasesModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesModel_DonationsId",
                table: "PurchasesModel",
                column: "DonationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesModel_DonationsModel_DonationsId",
                table: "PurchasesModel",
                column: "DonationsId",
                principalTable: "DonationsModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
