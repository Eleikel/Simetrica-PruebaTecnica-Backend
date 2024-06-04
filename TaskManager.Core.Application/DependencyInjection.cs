using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManager.Core.Application.Interfaces.Service;
using TaskManager.Core.Application.Services;

namespace TaskManager.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITasksService, TaskService>();
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }

        public static IServiceCollection AddDtoMaping(this IServiceCollection services)
        {           
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
