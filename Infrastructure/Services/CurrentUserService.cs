using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // access HttpContext 
        public int? UserId => GetUserId();

        private int? GetUserId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public bool IsAuthenticated => GetAuthenticated();

        private bool GetAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User.Identity != null &&
                    _httpContextAccessor.HttpContext != null &&
                    _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }


        public string Email => _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public string FullName => _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value + " " +
                                  _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        public bool IsAdmin => GetIsAdmin();

        private bool GetIsAdmin()
        {
            var roles = Roles;
            return roles.Any(r => r.Contains("Admin"));
        }

        public IEnumerable<string> Roles => GetRoles();
        public string UserName => GetName();
        private string GetName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name ??
                _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public string RemoteIpAddress => GetRemoteAddress();

        private string GetRemoteAddress()
        {
            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }

        public bool IsSuperAdmin => GetIsSuperAdmin();

        private bool GetIsSuperAdmin()
        {
            var roles = Roles;
            return roles.Any(r => r.Contains("SuperAdmin"));
        }

        public string ProfilePictureUrl { get; set; }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext?.User.Claims;
        }

        private IEnumerable<string> GetRoles()
        {
            var claims = GetClaimsIdentity();
            var roles = new List<string>();
            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    roles.Add(claim.Value);
                }
            }
            return roles;
        }
    }
}
