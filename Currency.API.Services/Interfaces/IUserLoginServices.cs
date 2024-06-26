using Currency.API.Models;
using Currency.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Services.Interfaces
{
    public interface IUserLoginServices
    {
        Task<UserModelDTO> UserLoginService(string email, string password);
        Task<UserModelDTO> GetUserByIdService(int userId);
    }
}
