using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Currency.ExchangeAPI.Models;
using Currency.API.Models;
using Newtonsoft.Json;

namespace Currency.API.Services
{
    public class CurrencyExchangeServices : ICurrencyExchangeServices
    {
        private readonly string _apiKey;
        private readonly IConfiguration _configuration;
        private readonly ICurrencyExchangeRepo _currencyExchangeRepo;
        private readonly HttpClient _httpClient;

        public CurrencyExchangeServices(
            ICurrencyExchangeRepo currencyExchangeRepo,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _currencyExchangeRepo = currencyExchangeRepo;
            _configuration = configuration;
            _httpClient = httpClient;
            _apiKey = _configuration["ExchangeRateApi:ApiKey"];
            _httpClient.BaseAddress = new System.Uri(_configuration["ExchangeRateApi:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CurrencyExchangeResponse> GetConvertedAmountServices(string baseCode, string targetCode, string amount)
        {
            var requestUrl = $"v6/{_apiKey}/pair/{baseCode}/{targetCode}/{amount}";
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var exchangeResponse = JsonConvert.DeserializeObject<CurrencyExchangeResponse>(responseContent);

            return exchangeResponse;
        }

        public async Task<CurrencyTypeModelAPI> getCurrencyByNameServices(string currencyTag)
        {
            var getCurrencyName = await _currencyExchangeRepo.getCurrencyByName(currencyTag);

            return getCurrencyName;
        }
    }
}