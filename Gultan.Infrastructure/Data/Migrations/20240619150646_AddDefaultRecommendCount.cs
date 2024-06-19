using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gultan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRecommendCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LastPrice",
                table: "Stocks",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "DefaultRecommendCount",
                table: "Stocks",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultRecommendCount",
                table: "Stocks");

            migrationBuilder.AlterColumn<decimal>(
                name: "LastPrice",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }
    }
}
