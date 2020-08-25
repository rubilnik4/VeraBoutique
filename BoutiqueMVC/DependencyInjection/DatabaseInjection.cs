using System;
using BoutiqueDAL.Factories.Implementations.Database.Boutique;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueMVC.Factories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей для баз данных
    /// </summary>
    public static class DatabaseInjection
    {
        /// <summary>
        /// Внедрить зависимости к базе данных
        /// </summary>
        public static void InjectPostgres (IServiceCollection services)
        {


            //var postgresConnection = PostgresConnectionFactory.PostgresConnection.Value;
            //string connection = $"Host={postgresConnection.Host};Port={postgresConnection.Port};Database={postgresConnection.Database};Username={postgresConnection.Username};Password={postgresConnection.Password}";
            //services.AddDbContext<BoutiqueEntityDatabase>(options => options.UseNpgsql(connection));

            services.AddTransient<IBoutiqueDatabase>(serviceProvider => new BoutiqueEntityDatabase());
            services.AddTransient<IGenderService>(serviceProvider => new GenderService(serviceProvider.GetService<IBoutiqueDatabase>()));
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static void UpdateSchema(IServiceProvider serviceProvider)
        {
            var boutiqueDatabase = serviceProvider.GetService<BoutiqueEntityDatabase>();
            boutiqueDatabase.Database.EnsureCreated();
        }
    }
}