using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaboBankingAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Categories_CategoryId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Categories_CategoryId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Categories_CategoryId",
                table: "Accounts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
