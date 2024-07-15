using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
    public class DeniedTransactionServices : IDeniedTransactionServices
    {
        //if user sends amount above limits transaction is stopped
        //if user sends amount to a account less than 2 mins old then transaction is stopped
    }
}