namespace Currency_WEB_API.Models
{
    public class CurrencyExchangeRequest
    {
        public string baseCurrency { get; set; }
        public string targetCurrency { get; set; }

        public string amount { get; set; }
    }
}
