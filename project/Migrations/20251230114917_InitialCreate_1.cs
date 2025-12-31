using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "DonationsModel");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "DonationsModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationsModel_CategoryId",
                table: "DonationsModel",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationsModel_CategoryModel_CategoryId",
                table: "DonationsModel",
                column: "CategoryId",
                principalTable: "CategoryModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationsModel_CategoryModel_CategoryId",
                table: "DonationsModel");

            migrationBuilder.DropTable(
                name: "CategoryModel");

            migrationBuilder.DropIndex(
                name: "IX_DonationsModel_CategoryId",
                table: "DonationsModel");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "DonationsModel");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "DonationsModel",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);
        }
    }
}
