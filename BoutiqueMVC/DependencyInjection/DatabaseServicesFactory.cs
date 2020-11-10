using System;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Фабрика создания сервисов базы данных
    /// </summary>
    public static class DatabaseServicesFactory
    {
        /// <summary>
        /// Получить сервис для типа пола одежды
        /// </summary>
        public static IGenderDatabaseService GetGenderService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new GenderDatabaseService(boutiqueDatabase,
                                                              boutiqueDatabase.GendersTable,
                                                              serviceProvider.GetService<IGenderEntityConverter>()));

        /// <summary>
        /// Получить сервис для категорий одежды
        /// </summary>
        public static ICategoryDatabaseService GetCategoryService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
                Map(boutiqueDatabase => new CategoryDatabaseService(boutiqueDatabase,
                                                                    boutiqueDatabase.CategoryTable,
                                                                    serviceProvider.GetService<ICategoryEntityConverter>()));

        /// <summary>
        /// Получить сервис для вида одежды
        /// </summary>
        public static IClothesTypeDatabaseService GetClothesTypeService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new ClothesTypeDatabaseService(boutiqueDatabase,
                                                                   boutiqueDatabase.ClotheTypeTable,
                                                                   boutiqueDatabase.GendersTable,
                                                                   boutiqueDatabase.CategoryTable,
                                                                   serviceProvider.GetService<IClothesTypeEntityConverter>()));

        /// <summary>
        /// Получить сервис для размеров одежды
        /// </summary>
        public static ISizeDatabaseService GetSizeService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new SizeDatabaseService(boutiqueDatabase,
                                                            boutiqueDatabase.SizeTable,
                                                            serviceProvider.GetService<ISizeEntityConverter>()));

        /// <summary>
        /// Получить сервис для группы размеров одежды
        /// </summary>
        public static ISizeGroupDatabaseService GetSizeGroupService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new SizeGroupDatabaseService(boutiqueDatabase,
                                                                 boutiqueDatabase.SizeGroupTable,
                                                                 serviceProvider.GetService<ISizeGroupEntityConverter>()));

        /// <summary>
        /// Получить сервис цвета одежды
        /// </summary>
        public static IColorClothesDatabaseService GetColorClothesService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new ColorClothesDatabaseService(boutiqueDatabase,
                                                                    boutiqueDatabase.ColorClothesTable,
                                                                    serviceProvider.GetService<IColorClothesEntityConverter>()));

        /// <summary>
        /// Получить сервис одежды
        /// </summary>
        public static IClothesDatabaseService GetClothesService(IServiceProvider serviceProvider) =>
            serviceProvider.GetService<IBoutiqueDatabase>().
            Map(boutiqueDatabase => new ClothesDatabaseService(boutiqueDatabase,
                                                               boutiqueDatabase.ClothesTable,
                                                               boutiqueDatabase.GendersTable, boutiqueDatabase.ClotheTypeTable,
                                                               serviceProvider.GetService<IGenderDatabaseService>(),
                                                               serviceProvider.GetService<IClothesTypeDatabaseService>(),
                                                               serviceProvider.GetService<IClothesShortEntityConverter>(),
                                                               serviceProvider.GetService<IClothesEntityConverter>()));
    }
}