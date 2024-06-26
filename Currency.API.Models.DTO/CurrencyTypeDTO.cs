using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Models.DTO
{
    public class CurrencyTypeDTO
    {
        public int CurrencyID { get; set; }
        public string CurrencyTag { get; set; }

        public decimal USD_Exchange { get; set; }

        public decimal EUR_Exchange { get; set; }

        public decimal GBP_Exchange { get; set; }
        public string Country { get; set; }
    }
}
