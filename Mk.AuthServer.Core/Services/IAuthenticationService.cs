using System.Threading.Tasks;
using Mk.AuthServer.Core.Dtos;
using SharedLibrary;
using SharedLibrary.Dtos;

namespace Mk.AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<Response<NoDataDto>> RevokeResfreshToken(string refreshToken);
        Response<ClientTokenDto> CreateClientTokenAsync(ClientLoginDto clientLoginDto);
    }
}