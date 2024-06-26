using Currency.API.Models.DTO;
using Currency.API.Repo.Interfaces;
using Currency.API.Services.Interfaces;

namespace Currency.API.Services
{
   public class UserInformationServices : IUserInformationServices
    {

        private readonly IUserInformationRepo _userInformationRepo;

        public UserInformationServices(IUserInformationRepo userInformationRepo)
        {
            _userInformationRepo = userInformationRepo;
        }
        public async Task<UserModelDTO> GetUserByTagService(string userTag)
        {
            var authUser = await _userInformationRepo.getUserByTagRepo(userTag);

            return new UserModelDTO
            {
                UserID = authUser.UserID,
                Email = authUser.Email,
                FirstName = authUser.FirstName,
                LastName = authUser.LastName,
                Mobile = authUser.Mobile,
                UserTag = authUser.UserTag,
                Status = authUser.Status
            };
        }
    }
}
