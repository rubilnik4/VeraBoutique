using System;
using System.Configuration;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueMVC.Factories.Identity;
using Functional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public static void InjectDatabaseServices(IServiceCollection services)
        {
            ConverterServices.InjectEntityConverters(services);

            services.AddTransient(typeof(IQueryableService<,>), typeof(QueryableService<,>));
            services.AddTransient(DatabaseServicesFactory.GetGenderService);
            services.AddTransient(DatabaseServicesFactory.GetCategoryService);
            services.AddTransient(DatabaseServicesFactory.GetClothesTypeService);
            services.AddTransient(DatabaseServicesFactory.GetSizeService);
            services.AddTransient(DatabaseServicesFactory.GetSizeGroupService);
            services.AddTransient(DatabaseServicesFactory.GetColorClothesService);
            services.AddTransient(DatabaseServicesFactory.GetClothesService);
            InjectDatabase(services);
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static async Task UpdateSchema(IServiceProvider serviceProvider) =>
            await serviceProvider.GetService<IBoutiqueDatabase>().
            UpdateSchema(serviceProvider.GetService<UserManager<IdentityUser>>(),
                         IdentityUserFactory.DefaultUsers);

        /// <summary>
        /// Подключить сервисы базы данных
        /// </summary>
        private static void InjectDatabase(IServiceCollection services)
        {
            if (PostgresConnection.HasErrors) throw new ConfigurationErrorsException(nameof(PostgresConnection));

            services.AddDbContext<BoutiqueEntityDatabase>(GetDatabaseOptions);
            services.AddTransient<IBoutiqueDatabase, BoutiqueEntityDatabase>();
        }

        /// <summary>
        /// Получить параметры подключения к базе данных
        /// </summary>
        private static void GetDatabaseOptions(DbContextOptionsBuilder options) =>
            options.
            UseNpgsql(PostgresConnection.Value.ConnectionString).
            EnableSensitiveDataLogging();
    }
}