using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Miniapp3.API
{
    public class SignService
    {
        public static SecurityKey GetSymetricSecurityKey(string securitKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitKey));
        }
    }
}