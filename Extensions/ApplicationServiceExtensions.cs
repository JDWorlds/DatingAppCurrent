using System.Text;
using datingApp.Data;
using datingApp.Interfaces;
using datingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace datingApp.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Adding Services AddDbContext, AddScoped
            services.AddDbContext<DataContext>(option=>
            {
                option.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
        
    }
}