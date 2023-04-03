using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaboBankingAppServer.Migrations
{
    /// <inheritdoc />
    public partial class CarbonMultiplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CarbonMultiplier",
                table: "Categories",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbonMultiplier",
                table: "Categories");
        }
    }
}
