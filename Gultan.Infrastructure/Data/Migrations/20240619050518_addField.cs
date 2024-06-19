using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gultan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SharePurchaseLimit",
                table: "Wallets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharePurchaseLimit",
                table: "Wallets");
        }
    }
}
