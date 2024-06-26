using Currency.API.Models;


namespace Currency.API.Repo.Interfaces
{
    public interface ICurrencyExchangeRepo
    {
        Task<CurrencyTypeModelAPI> getCurrencyById(int currencyId);
        Task<CurrencyTypeModelAPI> getCurrencyByName(string currencyTag);
    }
}
