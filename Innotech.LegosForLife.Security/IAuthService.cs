using System.Collections.Generic;
using InnoTech.LegosForLife.Security.Model;

namespace InnoTech.LegosForLife.Security
{
    public interface IAuthService
    {
        string GenerateJwtToken(LoginUser userUserName);
        string Hash(string password);
        List<Permission> GetPermissions(int userId);
    }
}