using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface IUserRequest
    {
        string AccessId { get; }
        string TenantId { get; }
        string Name { get; }
        string Email { get; }
        string[] Roles { get;  }

        bool HaveAllRoles(string[] roles);
        bool HaveSomeRole(string[] roles);

        IEnumerable<Claim> GetClaimsIdentity();
    }
}
