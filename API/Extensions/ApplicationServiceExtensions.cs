using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, string Dbconfig, IConfiguration config)
        {
            Services.AddScoped<ITokenService,TokenService>();
            // Add services to the container.
            Services.AddDbContext<DataContext>(options =>{
                options.UseSqlite(Dbconfig);
            });
            Services.AddCors();
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            Services.AddScoped<IPhotoService, PhotoService>();
            return Services;
        }
    }
}