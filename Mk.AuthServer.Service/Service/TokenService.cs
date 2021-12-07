using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mk.AuthServer.Core.Configuration;
using Mk.AuthServer.Core.Dtos;
using Mk.AuthServer.Core.models;
using Mk.AuthServer.Core.Services;
using SharedLibrary.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Mk.AuthServer.Service.Service
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOptions _tokenOptions;

        private string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        //Audience bu hangi apilere istek yapabileceğine karşılık gelir
        //Claimler Tokendaki Payloaddaki Bilgiler 
        /*
         *    "sub": "1234567890",
            "name": "John Doe",
            "iat": 1516239022
         */
        
        //Kullanıcı bilgisi ile alakalı Tokenı CreateEdeceğimiz Nokta burasıdır :)
        private IEnumerable<Claim> GetClaim(UserApp userApp, List<string> audience)
        {
            //Payloada eklenenen Claimler Kullanıcı ile ilgili bilgileri tutuyor.
            var userlist = new List<Claim>()
            {
                // "Id:UserId"
                new Claim(ClaimTypes.NameIdentifier, userApp.Id),
                // "email : UserEmail"
                // Client Bu kullanıcının Emailini almak istediği zaman başka bir endpointe istek 
                //yapmasına gerek yok zaten  base64 ile encode edilmiş tokenın payloadunda decode ettiğinde bu emaili alabilsin bunun için ekstradan veri tabanına gitmesini istemiyorum
                new Claim(JwtRegisteredClaimNames.Email, userApp.Email),
                new Claim(ClaimTypes.Name, userApp.UserName),
                //Claim Typları bu şekilde Identity Tarafından genereta edilmişleri yazmanın avantajı bulunmaktadır
                //Bu avantaj Örneğin Controllarda ben bu bilgiye direk dbye vs başka bir yere gitmeden Token üzerinden ulaşabilirim
                //Örnek User.Identity.Name (Acces Edebiliriz bu şekilde)
                //Token ıd
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            //Api bu tokenın audience bilgisine bakıcak ona uygun olup olmadığına göre requestleri handle edicek
            userlist.AddRange(audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud,x)));

            return userlist;
        }

        private IEnumerable<Claim> GetClientClaim(Client client)
        {
            var list = new List<Claim>();
            list.AddRange(client.Audience.Select((x => new Claim(JwtRegisteredClaimNames.Aud,x))));
            list.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            list.Add(new Claim(JwtRegisteredClaimNames.Sub,client.ClientId));
            //Sub == Subject :)
            return list;
        }

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOptions> options)
        {
            _userManager = userManager;
            _tokenOptions = options.Value;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            var tokenExpirationDate = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var refreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(_tokenOptions.RefreshTokenExpiration);
            var securityKey = SignService.GetSymetricSecurityKey(_tokenOptions.SecurityKey);

            SigningCredentials signingCredentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires:tokenExpirationDate,
                notBefore:DateTime.UtcNow,
                claims:GetClaim(userApp, _tokenOptions.Audience),
                signingCredentials:signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccesTokenExpirationDate = tokenExpirationDate,
                RefreshTokenExpirationDate = refreshTokenExpirationDate
            };
            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var tokenExpirationDate = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SignService.GetSymetricSecurityKey(_tokenOptions.SecurityKey);

            SigningCredentials signingCredentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires: tokenExpirationDate,
                notBefore: DateTime.Now,
                claims: GetClientClaim(client),
                signingCredentials:signingCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            var clientTokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccesTokenExpirationDate = tokenExpirationDate,
            };

            return clientTokenDto;
        }
    }
}