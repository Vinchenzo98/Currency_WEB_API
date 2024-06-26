namespace Currency.API.Models.DTO
{
    public class BlockedTransactionDTO
    {
        public int AccountID { get; set; }
        public int AdminID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BlockedTime { get; set; }
        public int CurrencyID { get; set; }
        public string Reason { get; set; }
        public DateTime TimeSent { get; set; }
        public DateTime UnBlockedTime { get; set; }
        public int UserID { get; set; }
    }
}