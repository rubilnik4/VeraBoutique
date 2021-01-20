﻿using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей конвертеров
    /// </summary>
    public static class ConverterServicesRegistration
    {
        /// <summary>
        ///  Внедрить зависимости трансферных конвертеров
        /// </summary>
        public static void RegisterTransferConverters(IServiceCollection services)
        {
            services.AddTransient<IGenderTransferConverter, GenderTransferConverter>();
            services.AddTransient<IClothesTypeTransferConverter, ClothesTypeTransferConverter>();
            services.AddTransient<IClothesTypeShortTransferConverter, ClothesTypeShortTransferConverter>();
            services.AddTransient<ICategoryTransferConverter, CategoryTransferConverter>();
            services.AddTransient<ISizeTransferConverter, SizeTransferConverter>();
            services.AddTransient<ISizeGroupShortTransferConverter, SizeGroupShortTransferConverter>();
            services.AddTransient<ISizeGroupTransferConverter, SizeGroupTransferConverter>();
            services.AddTransient<IColorTransferConverter, ColorTransferConverter>();
            services.AddTransient<IClothesShortTransferConverter, ClothesShortTransferConverter>();
            services.AddTransient<IClothesTransferConverter, ClothesTransferConverter>();
        }

        /// <summary>
        ///  Внедрить зависимости конвертеров сущностей базы данных
        /// </summary>
        public static void RegisterEntityConverters(IServiceCollection services)
        {
            services.AddTransient<IGenderEntityConverter, GenderEntityConverter>();
            services.AddTransient<IClothesTypeEntityConverter, ClothesTypeEntityConverter>();
            services.AddTransient<IClothesTypeShortEntityConverter, ClothesTypeShortEntityConverter>();
            services.AddTransient<ICategoryEntityConverter, CategoryEntityConverter>();
            services.AddTransient<ISizeEntityConverter, SizeEntityConverter>();
            services.AddTransient<ISizeGroupEntityConverter, SizeGroupEntityConverter>();
            services.AddTransient<ISizeGroupShortEntityConverter, SizeGroupShortEntityConverter>();
            services.AddTransient<IColorClothesEntityConverter, ColorClothesEntityConverter>();
            services.AddTransient<IClothesShortEntityConverter, ClothesShortEntityConverter>();
            services.AddTransient<IClothesEntityConverter, ClothesEntityConverter>();
        }
    }
}