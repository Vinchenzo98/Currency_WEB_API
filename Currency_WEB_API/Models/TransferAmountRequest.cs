namespace Currency_WEB_API.Models
{
    public class TransferAmountRequest
    {
        public decimal amount { get; set; }
        public string currencyTag { get; set; }
        public string userTag { get; set; }
    }
}