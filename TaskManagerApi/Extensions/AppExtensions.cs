using Swashbuckle.AspNetCore.SwaggerUI;

namespace TaskManager.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestor de Tareas API");
                opt.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}
