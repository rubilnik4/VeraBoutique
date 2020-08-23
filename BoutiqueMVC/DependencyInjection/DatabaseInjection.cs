using System;
using BoutiqueDAL.Factories.Implementations;
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
            
           
            var postgresConnection = PostgresConnectionFactory.PostgresConnection.Value;
            string connection = $"Host={postgresConnection.Host};Port={postgresConnection.Port};Database={postgresConnection.Database};Username={postgresConnection.Username};Password={postgresConnection.Password}";
            services.AddDbContext<BoutiqueDatabase>(options => options.UseNpgsql(connection));

            services.AddTransient<IGenderService, GenderService>();
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static void UpdateSchema(IServiceProvider serviceProvider)
        {
            var builder = new DbContextOptionsBuilder();
            var bb = builder.UseNpgsql("");

            var db = new BoutiqueDatabase(bb.Options);

            var boutiqueDatabase = serviceProvider.GetService<BoutiqueDatabase>();
            boutiqueDatabase.Database.EnsureCreated();


        }
    }
}