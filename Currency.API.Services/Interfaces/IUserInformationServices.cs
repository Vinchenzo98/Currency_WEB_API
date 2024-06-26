using Currency.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Services.Interfaces
{
    public interface IUserInformationServices
    {
        Task<UserModelDTO> GetUserByTagService(string userTag);
    }
}
