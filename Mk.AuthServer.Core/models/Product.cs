using System.ComponentModel.DataAnnotations;

namespace Mk.AuthServer.Core.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}