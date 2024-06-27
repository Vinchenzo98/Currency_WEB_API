namespace Currency_WEB_API.Models
{
    public class DepositRequest
    {
        public decimal Amount { get; set; }
        public string currencyTag { get; set; }
    }
}