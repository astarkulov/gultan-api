using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gultan.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Capital",
                table: "Wallets",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoalId",
                table: "Wallets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RiskLevel",
                table: "Wallets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_GoalId",
                table: "Wallets",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Goals_GoalId",
                table: "Wallets",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Goals_GoalId",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_GoalId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "Capital",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "GoalId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "RiskLevel",
                table: "Wallets");
        }
    }
}
