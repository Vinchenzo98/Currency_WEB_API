using Currency.API.DAL;
using Currency.API.Models;
using Currency.API.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Currency.API.Repo
{
    public class CurrencyExchangeRepo : ICurrencyExchangeRepo
    {
        private readonly CurrencyAPIContext _currencyAPIContext;

        public CurrencyExchangeRepo(CurrencyAPIContext currencyAPIContext)
        {
            _currencyAPIContext = currencyAPIContext;
        }

        public async Task<CurrencyTypeModelAPI> getCurrencyByName(string currencyTag)
        {

            var currency = await _currencyAPIContext.Currency.FirstOrDefaultAsync(c => c.CurrencyTag == currencyTag);

            if (currency == null)
            {
                return null;
            }
            return currency;
        }
        public async Task<CurrencyTypeModelAPI> getCurrencyById(int currencyId)
        {
            var currency = await _currencyAPIContext.Currency.FirstOrDefaultAsync(c => c.CurrencyID == currencyId);
            if (currency == null)
            {
                return null;
            }
            return currency;
        }

    }
}
