using System.ComponentModel.DataAnnotations.Schema;


namespace Currency.API.Models
{
    public class TransactionLogModelAPI
    {

        [ForeignKey("AccountID")]
        public AccountTypeModelAPI AccountType { get; set; }
        public int AccountID { get; set; }

        [ForeignKey("CurrencyID")]

        public CurrencyTypeModelAPI CurrencyType { get; set; }
        public int CurrencyID { get; set; }

        [ForeignKey("UserID")]

        public UsersModelAPI Users { get; set; }
        public int UserID { get; set; }
        public DateTime TimeSent { get; set; }

        public decimal Amount { get; set; }
        public virtual ICollection<BlockedTransactionsModelAPI> BlockedTransactions { get; set; }
    }
}
