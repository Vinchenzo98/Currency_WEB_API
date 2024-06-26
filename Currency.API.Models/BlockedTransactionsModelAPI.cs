using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Currency.API.Models
{
    public class BlockedTransactionsModelAPI
    {
        public int AccountID { get; set; }

        public int AdminID { get; set; }

        [ForeignKey("AdminID")]
        public AdminsModelAPI Admins { get; set; }

        public decimal Amount { get; set; }

        public DateTime BlockedTime { get; set; }

        [Key]
        public int BlockedTransactionsID { get; set; }

        public int CurrencyID { get; set; }
        public string Reason { get; set; }
        public DateTime TimeSent { get; set; }
        public TransactionLogModelAPI TransactionLog { get; set; }
        public DateTime UnBlockedTime { get; set; }
        public int UserID { get; set; }
    }
}