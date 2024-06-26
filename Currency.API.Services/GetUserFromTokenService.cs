using Currency.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Currency.API.Services
{
    public class GetUserFromTokenService : IGetUserFromTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserFromTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserIdFromToken()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("userId")?.Value;

            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }
    }
}
