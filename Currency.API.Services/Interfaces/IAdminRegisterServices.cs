using Currency.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Services.Interfaces
{
    public interface IAdminRegisterServices
    {
        Task<AdminModelDTO> RegisterAdminServices
        (
        string FirstName,
        string LastName,
        string Mobile,
        string Email,
        string Password
            );
    }
}
