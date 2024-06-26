using System.ComponentModel.DataAnnotations;

namespace Currency.API.Models
{
    public class CurrencyTypeModelAPI
    {
        [Key]
        public int CurrencyID { get; set; }
        [Required]
        public string CurrencyTag { get; set; }
        public decimal USD_Exchange { get; set; }

        public decimal EUR_Exchange { get; set; }

        public decimal GBP_Exchange { get; set; }


        public string Country { get; set; }

        public virtual ICollection<AccountTypeModelAPI> accountTypeModelAPI { get; set; }
        public virtual ICollection<TransactionLogModelAPI> transactionLogModelAPIs { get; set; }
    }
}
