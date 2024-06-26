using Currency.API.Models.DTO;
using Currency.ExchangeAPI.Models;

namespace Currency.API.Services.Interfaces
{
    public interface ICurrencyExchangeServices
    {
        Task<CurrencyExchangeResponse> GetConvertedAmountServices(string baseCode, string targetCode, string amount);
        //Task<CurrencyTypeDTO> UpdateExchangeRateServices(decimal exchangeRate);
    }
}
