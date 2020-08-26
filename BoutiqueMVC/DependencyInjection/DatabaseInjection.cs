using System;
using BoutiqueDAL.Factories.Implementations.Database.Boutique;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Microsoft.Extensions.DependencyInjection;
using static BoutiqueMVC.Factories.Implementation.PostgresConnectionFactory;

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
            services.AddTransient<IBoutiqueDatabaseFactory>(serviceProvider => new BoutiqueDatabaseFactory(PostgresConnection));
            services.AddTransient(GetGenderService);
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static void UpdateSchema(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabaseFactory>().BoutiqueDatabase.
            ResultVoidOk(boutiqueDatabase => boutiqueDatabase.UpdateSchema());
      

        /// <summary>
        /// Получить сервис для типа пола одежды
        /// </summary>
        private static IGenderService GetGenderService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabaseFactory>().BoutiqueDatabase.
            Map(boutiqueDatabase => new GenderService(boutiqueDatabase));
    }
}