using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class AutoMapperExtention
    {
        public static void AddAutoMapperService(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfiles()); });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
