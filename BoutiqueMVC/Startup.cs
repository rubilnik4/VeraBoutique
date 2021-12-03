using System;
using System.Threading.Tasks;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueMVC.DependencyInjection;
using BoutiqueMVC.Factories.Swagger;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Параметры конфигурации
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Регистрация зависимостей
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            ConverterServicesRegistration.RegisterConverters(services);
            ServicesRegistration.RegisterServices(services);
            IdentityServicesRegistration.RegistrationIdentityServices(services);
            DatabaseServicesRegistration.RegisterDatabaseServices(services);
            AuthorizeServicesRegistration.RegisterAuthorizeServices(services, Configuration);
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerDocument(SwaggerConfiguration.ConfigSwagger);
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

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

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
            await DatabaseServicesRegistration.UpdateSchema(serviceProvider);
    }
}
