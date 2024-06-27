﻿using Currency.API.Services.Interfaces;
using Currency_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController : Controller
    {
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly ICurrencyExchangeServices _currencyExchangeServices;
        private readonly IGetUserFromTokenService _userFromTokenService;
        private readonly IUserLoginServices _userLoginServices;

        public CurrencyExchangeController(
            ICurrencyExchangeServices currencyExchangeServices,
            IGetUserFromTokenService userFromTokenService,
            IUserLoginServices userLoginServices,
            IAccountTypeServices accountTypeServices
            )
        {
            _currencyExchangeServices = currencyExchangeServices;
            _userFromTokenService = userFromTokenService;
            _userLoginServices = userLoginServices;
            _accountTypeServices = accountTypeServices;
        }

        [HttpPost("exchange")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> ExchangeCurrencies(CurrencyExchangeRequest currencyExchange)
        {
            var userId = _userFromTokenService.GetUserIdFromToken();

            var user = await _userLoginServices.GetUserByIdService(userId);

            if (user == null)
            {
                return Unauthorized(userId + "not found");
            }
            var getExchangeRate = await _currencyExchangeServices.GetConvertedAmountServices(currencyExchange.baseCurrency, currencyExchange.targetCurrency, currencyExchange.amount);

            Decimal.TryParse(currencyExchange.amount, out decimal amountToDecimal);

            decimal convertToNegative = -Math.Abs(amountToDecimal);

            var amountToExchange = await _accountTypeServices.updateAmountServices(convertToNegative, userId, currencyExchange.baseCurrency);

            if (amountToExchange == null)
            {
                return BadRequest("Account balance is negative: " + amountToExchange.Amount + " transfer not allowed");
            }

            var exchangedAmount = await _accountTypeServices.updateAmountServices(getExchangeRate.ConversionResult, userId, currencyExchange.targetCurrency);

            if (exchangedAmount == null)
            {
                return BadRequest($"Account with currency type {exchangedAmount.AccountType} allready exists");
            }

            return Ok(exchangedAmount);
        }
    }
}