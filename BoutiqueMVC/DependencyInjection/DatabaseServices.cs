using System;
using System.Configuration;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueMVC.Factories.Identity;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
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
            services.AddTransient<IGenderEntityConverter>(serviceProvider => new GenderEntityConverter());
            services.AddTransient(GetGenderService);
            InjectDatabase(services);
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public static async Task UpdateSchema(IServiceProvider serviceProvider) =>
            await serviceProvider.GetService<IBoutiqueDatabase>().
            UpdateSchema(serviceProvider.GetService<UserManager<IdentityUser>>(),
                         IdentityUserFactory.DefaultUsers);

        private static void InjectDatabase(IServiceCollection services)
        {
            if (PostgresConnection.HasErrors) throw new ConfigurationErrorsException(nameof(PostgresConnection));

            services.AddDbContext<BoutiqueEntityDatabase>(GetDatabaseOptions);
            InjectDatabaseIdentities(services);
            services.AddTransient<IBoutiqueDatabase, BoutiqueEntityDatabase>();
        }

        /// <summary>
        /// Подключить сервисы авторизации к базе
        /// </summary>
        private static void InjectDatabaseIdentities(IServiceCollection services) =>
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                     {
                         options.Password.RequiredLength = IdentitySettings.PASSWORD_MINLENGTH;
                         options.SignIn.RequireConfirmedEmail = true;
                         options.User.RequireUniqueEmail = true;
                     }).
            AddEntityFrameworkStores<BoutiqueEntityDatabase>().
            AddDefaultTokenProviders();

        /// <summary>
        /// Получить параметры подключения к базе данных
        /// </summary>
        private static void GetDatabaseOptions(DbContextOptionsBuilder options) =>
            options.
            UseNpgsql(PostgresConnection.Value.ConnectionString).
            UseSnakeCaseNamingConvention();

        /// <summary>
        /// Получить сервис для типа пола одежды
        /// </summary>
        private static IGenderDatabaseService GetGenderService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new GenderDatabaseService(boutiqueDatabase,
                                                              boutiqueDatabase.GendersTable,
                                                              new GenderEntityConverter()));
    }
}