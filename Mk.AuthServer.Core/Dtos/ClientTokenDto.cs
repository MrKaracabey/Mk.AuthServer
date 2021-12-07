using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mk.AuthServer.Core.Extensions;

namespace Mk.AuthServer.Core.Dtos
{
    //Üyelik Sistemi Olmaz ise Direk Onun Yerine Kullanılabilir :)
    //Üyelik Gerektirmeyen Yerlere ise bu Token ile İstek ATICAZ
    public class ClientTokenDto
    {
        public string AccessToken { get; set; }
        
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime AccesTokenExpirationDate { get; set; }
    }
}