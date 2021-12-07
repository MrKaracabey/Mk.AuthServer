using System;
using Mk.AuthServer.Core.Extensions;

namespace Mk.AuthServer.Core.Dtos
{
    public class TokenDto
    {
        //Tokenın Payloadından bazı bilgiler Client olarak alınabilir :))))
        public string AccessToken { get; set; }
        
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime AccesTokenExpirationDate { get; set; }
        
        
        public string? RefreshToken { get; set; }
        
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}