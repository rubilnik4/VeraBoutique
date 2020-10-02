using System;
using System.Threading.Tasks;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueMVC.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            ControllerServices.InjectControllerServices(services);
            DatabaseServices.InjectDatabaseServices(services);
            AuthServices.AddAuthorization(services);
            AuthServices.InjectJwtServices(services, Configuration);
            AuthServices.InjectDatabaseIdentities(services);
            services.AddControllers();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Стартовые асинхронные операции
        /// </summary>
        public static async Task PreLoadAsync(IServiceProvider serviceProvider) =>
            await DatabaseServices.UpdateSchema(serviceProvider);
    }
}
