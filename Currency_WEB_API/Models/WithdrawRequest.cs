﻿namespace Currency_WEB_API.Models
{
    public class WithdrawRequest
    {
        public decimal Amount { get; set; }
        public string currencyTag { get; set; }
    }
}