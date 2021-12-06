using System;

namespace Mk.AuthServer.Core.Dtos
{
    //Üyelik Sistemi Olmaz ise Direk Onun Yerine Kullanılabilir :)
    //Üyelik Gerektirmeyen Yerlere ise bu Token ile İstek ATICAZ
    public class ClientTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccesTokenExpirationDate { get; set; }
    }
}