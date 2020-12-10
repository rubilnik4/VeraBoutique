using System;
using System.Configuration;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
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
            DatabaseTables.InjectDatabaseTables(services);

            InjectCommonServices(services);
            InjectValidateServices(services);
            InjectDatabase(services);
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static async Task UpdateSchema(IServiceProvider serviceProvider) =>
            await serviceProvider.GetService<IBoutiqueDatabase>()!.
            UpdateSchema(serviceProvider.GetService<UserManager<IdentityUser>>()!,
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
        /// Подключить общие сервисы базы данных
        /// </summary>
        private static void InjectCommonServices(IServiceCollection services)
        {
            services.AddTransient<IGenderDatabaseService, GenderDatabaseService>();
            services.AddTransient<ICategoryDatabaseService, CategoryDatabaseService>();
            services.AddTransient<IClothesTypeDatabaseService, ClothesTypeDatabaseService>();
            services.AddTransient<ISizeDatabaseService, SizeDatabaseService>();
            services.AddTransient<ISizeGroupDatabaseService, SizeGroupDatabaseService>();
            services.AddTransient<IColorClothesDatabaseService, ColorClothesDatabaseService>();
            services.AddTransient<IClothesDatabaseService, ClothesDatabaseService>();
        }

        /// <summary>
        /// Подключить сервисы проверок базы данных
        /// </summary>
        private static void InjectValidateServices(IServiceCollection services)
        {
            services.AddTransient<IGenderDatabaseValidateService, GenderDatabaseValidateService>();
            services.AddTransient<ICategoryDatabaseValidateService, CategoryDatabaseValidateService>();
            services.AddTransient<IClothesTypeDatabaseValidateService, ClothesTypeDatabaseValidateService>();
            services.AddTransient<ISizeDatabaseValidateService, SizeDatabaseValidateService>();
            services.AddTransient<ISizeGroupDatabaseValidateService, SizeGroupDatabaseValidateService>();
            services.AddTransient<IColorClothesDatabaseValidateService, ColorClothesDatabaseValidateService>();
            services.AddTransient<IClothesDatabaseValidateService, ClothesDatabaseValidateService>();
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