using System;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueMVC.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoutiqueMVC
{
    public class Startup
    {
        /// <summary>
        /// Параметры конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Регистрация зависимостей
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            DatabaseInjection.InjectPostgres(services);
        }

        /// <summary>
        /// Настройки
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            DatabaseInjection.UpdateSchema(serviceScope.ServiceProvider);
        }
    }
}
