using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HealthyHole.DAL;

namespace HealthyHole.API.Extensions
{
    public static class DalExtensions
    {
        public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlite("FileName=.\\DataBase.db")); //configuration.GetConnectionString("SqLiteConnection")
            
            return services;
        }
    }
}
