using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Mk.AuthServer.Service.Service
{
    public static class SignService
    {
        public static SecurityKey GetSymetricSecurityKey(string securitKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitKey));
        }
    }
}