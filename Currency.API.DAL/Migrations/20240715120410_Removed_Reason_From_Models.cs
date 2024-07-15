using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Currency.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Reason_From_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "BlockedUserLog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "BlockedUserLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
