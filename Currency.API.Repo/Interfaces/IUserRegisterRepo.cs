using Currency.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.API.Repo.Interfaces
{
    public interface IUserRegisterRepo
    {
        Task<UsersModelAPI> RegisterUserRepo(UsersModelAPI userModel);

        Task<UsersModelAPI> AuthRegisterRepo(string email);
    }
}
