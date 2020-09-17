using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Boutique;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Microsoft.Extensions.DependencyInjection;
using static BoutiqueMVC.Factories.Database.PostgresConnectionFactory;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей для баз данных
    /// </summary>
    public static class DatabaseServices
    {
        /// <summary>
        /// Внедрить зависимости к базе данных
        /// </summary>
        public static void InjectDatabase(IServiceCollection services)
        {
            services.AddTransient<IGenderEntityConverter>(serviceProvider => new GenderEntityConverter());
            services.AddTransient<IBoutiqueDatabaseFactory>(serviceProvider => new BoutiqueDatabaseFactory(PostgresConnection));
            services.AddTransient(GetGenderService);
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static void UpdateSchema(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabaseFactory>().BoutiqueDatabase.
            ResultValueVoidOk(boutiqueDatabase => boutiqueDatabase.UpdateSchema());

        /// <summary>
        /// Получить сервис для типа пола одежды
        /// </summary>
        private static IGenderDatabaseService GetGenderService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabaseFactory>().BoutiqueDatabase.
            Map(boutiqueDatabase => new GenderDatabaseService(boutiqueDatabase,
                                                      boutiqueDatabase.ResultValueOk(database => database.GendersTable),
                                                      new GenderEntityConverter()));
    }
}