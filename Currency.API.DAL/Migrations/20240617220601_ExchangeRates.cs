using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Currency.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTypes_CurrencyType_CurrencyID",
                table: "AccountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTypes_Users_UserID",
                table: "AccountTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_AccountTypes_AccountID",
                table: "BlockedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLog_AccountTypes_AccountID",
                table: "TransactionLog");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLog_CurrencyType_CurrencyID",
                table: "TransactionLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrencyType",
                table: "CurrencyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountTypes",
                table: "AccountTypes");

            migrationBuilder.RenameTable(
                name: "CurrencyType",
                newName: "Currency");

            migrationBuilder.RenameTable(
                name: "AccountTypes",
                newName: "AccountType");

            migrationBuilder.RenameColumn(
                name: "ExchangeRate",
                table: "Currency",
                newName: "USD_Exchange");

            migrationBuilder.RenameIndex(
                name: "IX_AccountTypes_UserID",
                table: "AccountType",
                newName: "IX_AccountType_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_AccountTypes_CurrencyID",
                table: "AccountType",
                newName: "IX_AccountType_CurrencyID");

            migrationBuilder.AddColumn<decimal>(
                name: "EUR_Exchange",
                table: "Currency",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GBP_Exchange",
                table: "Currency",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "CurrencyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountType",
                table: "AccountType",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountType_Currency_CurrencyID",
                table: "AccountType",
                column: "CurrencyID",
                principalTable: "Currency",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountType_Users_UserID",
                table: "AccountType",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_AccountType_AccountID",
                table: "BlockedUsers",
                column: "AccountID",
                principalTable: "AccountType",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLog_AccountType_AccountID",
                table: "TransactionLog",
                column: "AccountID",
                principalTable: "AccountType",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLog_Currency_CurrencyID",
                table: "TransactionLog",
                column: "CurrencyID",
                principalTable: "Currency",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountType_Currency_CurrencyID",
                table: "AccountType");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountType_Users_UserID",
                table: "AccountType");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_AccountType_AccountID",
                table: "BlockedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLog_AccountType_AccountID",
                table: "TransactionLog");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionLog_Currency_CurrencyID",
                table: "TransactionLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountType",
                table: "AccountType");

            migrationBuilder.DropColumn(
                name: "EUR_Exchange",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "GBP_Exchange",
                table: "Currency");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "CurrencyType");

            migrationBuilder.RenameTable(
                name: "AccountType",
                newName: "AccountTypes");

            migrationBuilder.RenameColumn(
                name: "USD_Exchange",
                table: "CurrencyType",
                newName: "ExchangeRate");

            migrationBuilder.RenameIndex(
                name: "IX_AccountType_UserID",
                table: "AccountTypes",
                newName: "IX_AccountTypes_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_AccountType_CurrencyID",
                table: "AccountTypes",
                newName: "IX_AccountTypes_CurrencyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrencyType",
                table: "CurrencyType",
                column: "CurrencyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountTypes",
                table: "AccountTypes",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTypes_CurrencyType_CurrencyID",
                table: "AccountTypes",
                column: "CurrencyID",
                principalTable: "CurrencyType",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTypes_Users_UserID",
                table: "AccountTypes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_AccountTypes_AccountID",
                table: "BlockedUsers",
                column: "AccountID",
                principalTable: "AccountTypes",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLog_AccountTypes_AccountID",
                table: "TransactionLog",
                column: "AccountID",
                principalTable: "AccountTypes",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionLog_CurrencyType_CurrencyID",
                table: "TransactionLog",
                column: "CurrencyID",
                principalTable: "CurrencyType",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
