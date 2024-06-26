
namespace Currency.API.Models.DTO
{
    public class TransactionLogDTO
    {
        public int AccountID { get; set; }
        public int CurrencyID { get; set; }
        public int UserID { get; set; }
        public DateTime TimeSent { get; set; }

        public decimal Amount { get; set; }
    }
}
