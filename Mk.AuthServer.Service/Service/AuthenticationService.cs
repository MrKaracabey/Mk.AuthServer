using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mk.AuthServer.Core.Configuration;
using Mk.AuthServer.Core.Dtos;
using Mk.AuthServer.Core.models;
using Mk.AuthServer.Core.Repositories;
using Mk.AuthServer.Core.Services;
using SharedLibrary;
using SharedLibrary.Dtos;
using IAuthenticationService = Mk.AuthServer.Core.Services.IAuthenticationService;

namespace Mk.AuthServer.Service.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshToken;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshToken)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshToken = userRefreshToken;
        }


        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Response<TokenDto>.Fail("Emaill or password Wrong", 400, true);
            }

            if (! await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Response<TokenDto>.Fail("Emaill or password Wrong", 400, true);
            }

            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshToken.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshToken.AddAsync(new UserRefreshToken
                {
                    UserId = user.Id,
                    Code = token.RefreshToken,
                    ExpirationDate = token.RefreshTokenExpirationDate
                });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.ExpirationDate = token.RefreshTokenExpirationDate;
            }

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token, 200);

        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshToken.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("RefreshToken Did not Found", 404, true);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return Response<TokenDto>.Fail("User Id did not Found", 404, true);
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.ExpirationDate = tokenDto.RefreshTokenExpirationDate;

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(tokenDto, 200);

        }

        public async Task<Response<NoDataDto>> RevokeResfreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshToken.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh Token did not found", 404, true);
            }
            
            _userRefreshToken.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);
        }

        public Response<ClientTokenDto> CreateClientTokenAsync(ClientLoginDto clientLoginDto)
        {
            var deneme = _clients.Count;
            var client = _clients.SingleOrDefault(x =>
                x.ClientId == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return Response<ClientTokenDto>.Fail("ClientId or ClientSecret Not Found", 404, true);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token, 200);
        }
    }
}