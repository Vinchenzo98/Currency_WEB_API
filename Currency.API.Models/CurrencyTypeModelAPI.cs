using System.ComponentModel.DataAnnotations;

namespace Currency.API.Models
{
    public class CurrencyTypeModelAPI
    {
        [Key]
        public int CurrencyID { get; set; }
        [Required]
        public string CurrencyTag { get; set; }

        public string Country { get; set; }

        public virtual ICollection<AccountTypeModelAPI> accountTypeModelAPI { get; set; }
        public virtual ICollection<TransactionLogModelAPI> transactionLogModelAPIs { get; set; }
    }
}
