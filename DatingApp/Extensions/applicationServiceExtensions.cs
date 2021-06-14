using DatingApp.Data;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using DatingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Extensions
{
    public static class applicationServiceExtensions
    {
        public static IServiceCollection addApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(autoMapperProfiles).Assembly);
            services.Configure<cloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<iTokenService, tokenService>();
            services.AddScoped<iPhotoService, photoService>();
            services.AddScoped<iUserRepository, userRepository>();
            services.AddControllers();
            services.AddDbContext<dataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("defaultConnection"));
            });
            return services;
        }
    }
}
