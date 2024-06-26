using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Models
{
    public class AccountTypeModelAPI
    {
        [Key]
        public int AccountID { get; set; }

        public string AccountType { get; set; }

        [ForeignKey("CurrencyID")]
        public CurrencyTypeModelAPI CurrencyType { get; set; }
        public int CurrencyID { get; set; }

        [ForeignKey("UserID")]
        public UsersModelAPI Users { get; set; }
        public int UserID { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<BlockedUserLogModelAPI> blockedUserLogsAPI {get; set;}
        public virtual ICollection<TransactionLogModelAPI> transactionLogModelAPIs { get; set; }

    }
}
