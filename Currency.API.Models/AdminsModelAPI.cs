using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Currency.API.Models
{
    public class AdminsModelAPI
    {
        [Key]
        public int AdminID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string IsValidEmail { get; set;}
        public virtual ICollection<BlockedUserLogModelAPI> blockedUserLogsAPI { get; set; }
        public virtual ICollection<BlockedTransactionsModelAPI> blockedTransactionsAPI { get; set; }
    }
}
