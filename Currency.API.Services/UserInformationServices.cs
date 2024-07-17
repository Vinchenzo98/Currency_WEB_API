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

        public async Task<List<UserModelDTO>> getAllUsersService()
        {
            var getUsers = _userInformationRepo.getAllUsers();

            var usersList = new List<UserModelDTO>();

            foreach (var user in getUsers)
            {
                var userDTO = new UserModelDTO()
                {
                    UserTag = user.UserTag,
                    Email = user.Email,
                    Status = user.Status,
                };
                usersList.Add(userDTO);
            }

            return usersList;
        }

        public async Task<UserModelDTO> getUserByEmailServices(string email)
        {
            var user = await _userInformationRepo.getUserByEmailRepo(email);

            return new UserModelDTO
            {
                Email = user.Email,
                UserID = user.UserID,
                UserTag = user.UserTag
            };
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

        public async Task<UserModelDTO> updateUserStatus(string userTag)
        {
            var getUser = await _userInformationRepo.getUserByTagRepo(userTag);

            var updateUser = await _userInformationRepo.changeUserStatus(getUser);

            return new UserModelDTO
            {
                UserTag = updateUser.UserTag,
                Status = updateUser.Status
            };
        }
    }
}