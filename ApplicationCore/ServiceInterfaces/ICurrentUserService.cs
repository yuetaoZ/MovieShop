using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        bool IsAuthenticated { get; }
        string UserName { get; }
        string Email { get; }
        string RemoteIpAddress { get; }
        string FullName { get; }
        bool IsAdmin { get; }
        bool IsSuperAdmin { get; }
        IEnumerable<string> Roles { get; }
        IEnumerable<Claim> GetClaimsIdentity();
        string ProfilePictureUrl { get; set; }
    }
}
