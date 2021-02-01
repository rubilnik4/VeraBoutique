using System;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueXamarin.Infrastructure.Implementations;
using BoutiqueXamarin.Infrastructure.Implementations.Converters;
using BoutiqueXamarin.Infrastructure.Interfaces.Converters;
using Prism.Ioc;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей конвертеров
    /// </summary>
    public static class ConverterServicesRegistration
    {
        /// <summary>
        /// Регистрация конвертеров трансферных моделей
        /// </summary>
        public static void RegisterTransferConverters(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGenderTransferConverter, GenderTransferConverter>();
            containerRegistry.Register<IClothesTypeTransferConverter, ClothesTypeTransferConverter>();
            containerRegistry.Register<IClothesTypeShortTransferConverter, ClothesTypeShortTransferConverter>();
            containerRegistry.Register<ICategoryTransferConverter, CategoryTransferConverter>();
            containerRegistry.Register<ISizeTransferConverter, SizeTransferConverter>();
            containerRegistry.Register<ISizeGroupShortTransferConverter, SizeGroupShortTransferConverter>();
            containerRegistry.Register<ISizeGroupTransferConverter, SizeGroupTransferConverter>();
            containerRegistry.Register<IColorTransferConverter, ColorTransferConverter>();
            containerRegistry.Register<IClothesShortTransferConverter, ClothesShortTransferConverter>();
            containerRegistry.Register<IClothesTransferConverter, ClothesTransferConverter>();

            containerRegistry.Register<IHostConfigurationTransferConverter, HostConfigurationTransferConverter>();
            containerRegistry.Register<IXamarinConfigurationTransferConverter, XamarinConfigurationTransferConverter>();
        }
    }
}