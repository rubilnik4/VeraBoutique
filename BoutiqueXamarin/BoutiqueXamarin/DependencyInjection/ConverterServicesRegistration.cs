using System;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
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
        public static void RegisterTransferConverters(IBoutiqueContainer container)
        {
            container.Register<IGenderTransferConverter, GenderTransferConverter>();
            container.Register<IClothesTypeTransferConverter, ClothesTypeTransferConverter>();
            container.Register<IClothesTypeShortTransferConverter, ClothesTypeShortTransferConverter>();
            container.Register<ICategoryTransferConverter, CategoryTransferConverter>();
            container.Register<ISizeTransferConverter, SizeTransferConverter>();
            container.Register<ISizeGroupShortTransferConverter, SizeGroupShortTransferConverter>();
            container.Register<ISizeGroupTransferConverter, SizeGroupTransferConverter>();
            container.Register<IColorTransferConverter, ColorTransferConverter>();
            container.Register<IClothesShortTransferConverter, ClothesShortTransferConverter>();
            container.Register<IClothesTransferConverter, ClothesTransferConverter>();

            container.Register<IHostConfigurationTransferConverter, HostConfigurationTransferConverter>();
            container.Register<IXamarinConfigurationTransferConverter, XamarinConfigurationTransferConverter>();
        }
    }
}