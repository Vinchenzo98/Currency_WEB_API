﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Models.DTO
{
    public class AccountTypeDTO
    {
        public int AccountID { get; set; }
        public string AccountType { get; set; }

        public decimal Amount { get; set; }
        public int CurrencyID { get; set; }
        public int UserID { get; set; }
    }
}