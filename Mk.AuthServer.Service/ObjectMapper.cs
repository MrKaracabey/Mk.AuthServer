using System;
using AutoMapper;

namespace Mk.AuthServer.Service
{
    public static class ObjectMapper
    {
        //Lazy oldu yani Kullanına kadar memorye yüklenmeyecek
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}