﻿using System;
using System.Configuration;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
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

            services.AddTransient(GetGenderService);
            services.AddTransient(GetCategoryService);
            services.AddTransient(GetClothesTypeService);
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
            UseSnakeCaseNamingConvention();

        /// <summary>
        /// Получить сервис для типа пола одежды
        /// </summary>
        private static IGenderDatabaseService GetGenderService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new GenderDatabaseService(boutiqueDatabase,
                                                              boutiqueDatabase.GendersTable,
                                                              new GenderEntityConverter()));

        /// <summary>
        /// Получить сервис для категорий одежды
        /// </summary>
        private static ICategoryDatabaseService GetCategoryService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
                Map(boutiqueDatabase => new CategoryDatabaseService(boutiqueDatabase,
                                                                    boutiqueDatabase.CategoryTable,
                                                                    new CategoryEntityConverter()));

        /// <summary>
        /// Получить сервис для вида одежды
        /// </summary>
        private static IClothesTypeDatabaseService GetClothesTypeService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new ClothesTypeDatabaseService(boutiqueDatabase,
                                                                   boutiqueDatabase.ClotheTypeTable,
                                                                   boutiqueDatabase.GendersTable,
                                                                   new ClothesTypeEntityConverter()));
    }
}