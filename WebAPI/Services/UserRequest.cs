using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class UserRequest : IUserRequest
    {
        private readonly IHttpContextAccessor _accessor;

        public UserRequest(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        public string Email => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
        public string[] Roles => _accessor.HttpContext.User.Claims.Where(x => x.Type == "role").Select(p => p.Value).ToArray();
        public string AccessId => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessId")?.Value;
        public string TenantId => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "tenantId")?.Value;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public bool HaveAllRoles(string[] roles)
        {
            foreach (var role in roles)
            {
                if (!_accessor.HttpContext.User.IsInRole(role)) return false;
            }
            return true;
        }

        public bool HaveSomeRole(string[] roles)
        {
            foreach (var role in roles)
            {
                if (_accessor.HttpContext.User.IsInRole(role)) return true;
            }
            return false;
        }
    }
}
