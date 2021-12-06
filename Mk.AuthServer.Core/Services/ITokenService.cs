using Mk.AuthServer.Core.Configuration;
using Mk.AuthServer.Core.Dtos;
using Mk.AuthServer.Core.models;

namespace Mk.AuthServer.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);
        ClientTokenDto CreateTokenByClient(Client client);

    }
}