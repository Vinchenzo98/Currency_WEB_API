namespace Currency_WEB_API.Models
{
    public class BlockTransactionRequest
    {
        public string currencyTag { get; set; }
        public string Reason { get; set; }
        public string userTag { get; set; }
    }
}