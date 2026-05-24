using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Liopleurodons_Pocket_Business_Helper.Migrations
{
    /// <inheritdoc />
    public partial class AddIsIncomeToPurchases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "Purchases",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "Purchases");
        }
    }
}
