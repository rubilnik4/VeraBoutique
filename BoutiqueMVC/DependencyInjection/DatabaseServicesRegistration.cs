using System;
using System.Configuration;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Carts;
using BoutiqueDAL.Infrastructure.Implementations.Services.Carts.Validate;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Implementations.Services.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueMVC.Factories.Database;
using BoutiqueMVC.Factories.Identities;
using BoutiqueMVC.Infrastructure.Implementation.Carts;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using BoutiqueMVC.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static BoutiqueMVC.Factories.Database.PostgresConnectionFactory;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей для баз данных
    /// </summary>
    public static class DatabaseServicesRegistration
    {
        /// <summary>
        /// Внедрить зависимости к базе данных
        /// </summary>
        public static void RegisterDatabaseServices(IServiceCollection services)
        {
            DatabaseTablesRegistration.RegisterDatabaseTables(services);
            RegisterMainDatabaseServices(services);
            RegisterValidateServices(services);
            RegisterDatabase(services);
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static async Task UpdateSchema(IServiceProvider serviceProvider) =>
            await serviceProvider.GetService<IBoutiqueDatabase>()!.
            UpdateSchema(serviceProvider.GetService<IUserManagerService>()!, serviceProvider.GetService<IRoleStoreService>()!,
                         AuthorizeFactory.DefaultUsers, AuthorizeFactory.RoleNames);

        /// <summary>
        /// Подключить сервисы базы данных
        /// </summary>
        private static void RegisterDatabase(IServiceCollection services)
        {
            services.AddDbContext<BoutiqueDatabase>(GetDatabaseOptions);
            services.AddTransient<IBoutiqueDatabase, BoutiqueDatabase>();
        }

        /// <summary>
        /// Подключить общие сервисы базы данных
        /// </summary>
        private static void RegisterMainDatabaseServices(IServiceCollection services)
        {
            services.AddTransient<IGenderDatabaseService, GenderDatabaseService>();
            services.AddTransient<ICategoryDatabaseService, CategoryDatabaseService>();
            services.AddTransient<IClothesTypeDatabaseService, ClothesTypeDatabaseService>();
            services.AddTransient<ISizeDatabaseService, SizeDatabaseService>();
            services.AddTransient<ISizeGroupDatabaseService, SizeGroupDatabaseService>();
            services.AddTransient<IColorDatabaseService, ColorDatabaseService>();
            services.AddTransient<IClothesDatabaseService, ClothesDatabaseService>();
            services.AddTransient<ICartDatabaseService, CartDatabaseService>();
        }

        /// <summary>
        /// Подключить сервисы проверок базы данных
        /// </summary>
        private static void RegisterValidateServices(IServiceCollection services)
        {
            services.AddTransient<IGenderDatabaseValidateService, GenderDatabaseValidateService>();
            services.AddTransient<ICategoryDatabaseValidateService, CategoryDatabaseValidateService>();
            services.AddTransient<IClothesTypeDatabaseValidateService, ClothesTypeDatabaseValidateService>();
            services.AddTransient<ISizeDatabaseValidateService, SizeDatabaseValidateService>();
            services.AddTransient<ISizeGroupDatabaseValidateService, SizeGroupDatabaseValidateService>();
            services.AddTransient<IColorClothesDatabaseValidateService, ColorClothesDatabaseValidateService>();
            services.AddTransient<IClothesDatabaseValidateService, ClothesDatabaseValidateService>();
            services.AddTransient<IClothesImageDatabaseValidateService, ClothesImageDatabaseValidateService>();
            services.AddTransient<ICartDatabaseValidateService, CartDatabaseValidateService>();
        }

        /// <summary>
        /// Получить параметры подключения к базе данных
        /// </summary>
        private static void GetDatabaseOptions(DbContextOptionsBuilder options) =>
            options.
            UseNpgsql(PostgresConnection.ConnectionString).
            EnableSensitiveDataLogging();
    }
}