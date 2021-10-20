using System;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Converters;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Converters;
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
            container.Register<IAuthorizeTransferConverter, AuthorizeTransferConverter>();
            container.Register<IPersonalTransferConverter, PersonalTransferConverter>();
            container.Register<IBoutiqueUserTransferConverter, BoutiqueUserTransferConverter>();
            container.Register<IRegisterTransferConverter, RegisterTransferConverter>();
            container.Register<IGenderTransferConverter, GenderTransferConverter>();
            container.Register<IGenderCategoryTransferConverter, GenderCategoryTransferConverter>();
            container.Register<IClothesTypeTransferConverter, ClothesTypeTransferConverter>();
            container.Register<IClothesTypeMainTransferConverter, ClothesTypeMainTransferConverter>();
            container.Register<ICategoryTransferConverter, CategoryTransferConverter>();
            container.Register<ICategoryClothesTypeTransferConverter, CategoryClothesTypeTransferConverter>();
            container.Register<ISizeTransferConverter, SizeTransferConverter>();
            container.Register<ISizeGroupMainTransferConverter, SizeGroupMainTransferConverter>();
            container.Register<ISizeGroupTransferConverter, SizeGroupTransferConverter>();
            container.Register<IColorTransferConverter, ColorTransferConverter>();
            container.Register<IClothesTransferConverter, ClothesTransferConverter>();
            container.Register<IClothesDetailTransferConverter, ClothesDetailTransferConverter>();
            container.Register<IClothesImageTransferConverter, ClothesImageTransferConverter>();
            container.Register<IClothesMainTransferConverter, ClothesMainTransferConverter>();

            container.Register<IHostConfigurationTransferConverter, HostConfigurationTransferConverter>();
            container.Register<IXamarinConfigurationTransferConverter, XamarinConfigurationTransferConverter>();
        }
    }
}