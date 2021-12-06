using System;

namespace Mk.AuthServer.Core.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccesTokenExpirationDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}