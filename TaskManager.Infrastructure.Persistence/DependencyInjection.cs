using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Persistence.Repositories;
using TaskManager.Infrastructure.Persistence.Context;
using TaskManager.Infrastructure.Persistence.Interfaces.Repository;
using TaskManager.Common;

namespace TaskManager.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseOracle(configuration["ConnectionStrings:DefaultConnection"]);
            });

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITasksRepository, TasksRepository>();
            //services.AddScoped<UserContext>();

            services.AddHttpContextAccessor();

            services.AddScoped<UserContext>();

            #endregion

            return services;
        }
    }
}
