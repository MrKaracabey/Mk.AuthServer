using System.Collections.Generic;

namespace Mk.AuthServer.Core.Configuration
{
    public class Client
    {
        // Hocam Buradaki Client Şu demek senin 2 türlü token mekanizman olucak
        // 1 - siteye üye olan kullanıcıların erişebileceği apiler ki bu normal AccessToken ve Refreshtoken
        // 2- Eğer Sen Siteye Üye değilsen erişebileceğin Endpointler olur bunlar da Client Olarak Adlandırdım
        public string ClientId { get; set; }
        public string Secret { get; set; }
        //Kimlere Erişebileceğini Tutacağız
        //api1 ile api2 ye erişebilir :)
        public List<string> Audience { get; set; }
    }
}