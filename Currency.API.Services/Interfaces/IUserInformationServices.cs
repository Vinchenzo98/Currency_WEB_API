using Currency.API.Models.DTO;

namespace Currency.API.Services.Interfaces
{
    public interface IUserInformationServices
    {
        Task<UserModelDTO> getUserByEmailServices(string email);

        Task<UserModelDTO> GetUserByTagService(string userTag);

        Task<UserModelDTO> updateUserStatus(string userTag);
    }
}