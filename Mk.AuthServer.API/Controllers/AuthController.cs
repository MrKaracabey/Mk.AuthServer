
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mk.AuthServer.Core.Dtos;
using Mk.AuthServer.Core.Services;

namespace Mk.AuthServer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public  IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result =  _authenticationService.CreateClientTokenAsync(clientLoginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeResfreshToken(refreshTokenDto.Token);

            return ActionResultInstance(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token);

            return ActionResultInstance(result);

        }
        
        
    }
}