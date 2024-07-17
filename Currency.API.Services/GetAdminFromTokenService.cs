using Currency.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Currency.API.Services
{
    public class GetAdminFromTokenService : IGetAdminTokenFromService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAdminFromTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetAdminIdFromToken()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var adminIdClaim = claimsIdentity?.FindFirst("adminId")?.Value;

            return int.TryParse(adminIdClaim, out var adminId) ? adminId : 0;
        }
    }
}