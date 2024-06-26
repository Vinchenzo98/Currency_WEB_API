using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Currency.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountToTransactionLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "TransactionLog",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransactionLog");
        }
    }
}
