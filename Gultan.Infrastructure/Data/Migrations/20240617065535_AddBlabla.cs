using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gultan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBlabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Long",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Middle",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Short",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Long",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Middle",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Short",
                table: "Stocks");
        }
    }
}
