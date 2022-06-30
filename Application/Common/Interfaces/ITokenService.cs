using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(
            string tenantId, 
            string accessId,
            string email,
            string name,
            string[] roles,
            string changePassword);
    }
}
