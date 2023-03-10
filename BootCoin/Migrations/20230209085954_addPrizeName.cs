using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BootCoin.Migrations
{
    /// <inheritdoc />
    public partial class addPrizeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrizeName",
                table: "Redeems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrizeName",
                table: "Redeems");
        }
    }
}
