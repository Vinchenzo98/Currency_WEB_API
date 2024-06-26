using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Currency.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Added_Account_To_Transaction_PK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedTransactions_TransactionLog_AccountID_UserID_CurrencyID_TimeSent",
                table: "BlockedTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionLog",
                table: "TransactionLog");

            migrationBuilder.DropIndex(
                name: "IX_BlockedTransactions_AccountID_UserID_CurrencyID_TimeSent",
                table: "BlockedTransactions");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "BlockedTransactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedTime",
                table: "BlockedTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "BlockedTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionLog",
                table: "TransactionLog",
                columns: new[] { "AccountID", "CurrencyID", "UserID", "TimeSent", "Amount" });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedTransactions_AccountID_UserID_CurrencyID_TimeSent_Amount",
                table: "BlockedTransactions",
                columns: new[] { "AccountID", "UserID", "CurrencyID", "TimeSent", "Amount" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedTransactions_TransactionLog_AccountID_UserID_CurrencyID_TimeSent_Amount",
                table: "BlockedTransactions",
                columns: new[] { "AccountID", "UserID", "CurrencyID", "TimeSent", "Amount" },
                principalTable: "TransactionLog",
                principalColumns: new[] { "AccountID", "CurrencyID", "UserID", "TimeSent", "Amount" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedTransactions_TransactionLog_AccountID_UserID_CurrencyID_TimeSent_Amount",
                table: "BlockedTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionLog",
                table: "TransactionLog");

            migrationBuilder.DropIndex(
                name: "IX_BlockedTransactions_AccountID_UserID_CurrencyID_TimeSent_Amount",
                table: "BlockedTransactions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "BlockedTransactions");

            migrationBuilder.DropColumn(
                name: "BlockedTime",
                table: "BlockedTransactions");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "BlockedTransactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionLog",
                table: "TransactionLog",
                columns: new[] { "AccountID", "CurrencyID", "UserID", "TimeSent" });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedTransactions_AccountID_UserID_CurrencyID_TimeSent",
                table: "BlockedTransactions",
                columns: new[] { "AccountID", "UserID", "CurrencyID", "TimeSent" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedTransactions_TransactionLog_AccountID_UserID_CurrencyID_TimeSent",
                table: "BlockedTransactions",
                columns: new[] { "AccountID", "UserID", "CurrencyID", "TimeSent" },
                principalTable: "TransactionLog",
                principalColumns: new[] { "AccountID", "CurrencyID", "UserID", "TimeSent" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
