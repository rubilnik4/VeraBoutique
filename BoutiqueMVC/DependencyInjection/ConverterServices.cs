using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntity;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfer;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfer;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfer;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей конвертеров
    /// </summary>
    public static class ConverterServices
    {
        /// <summary>
        ///  Внедрить зависимости трансферных конвертеров
        /// </summary>
        public static void InjectTransferConverters(IServiceCollection services)
        {
            services.AddTransient<IGenderTransferConverter, GenderTransferConverter>();
            services.AddTransient<IClothesTypeFullTransferConverter, ClothesTypeFullTransferConverter>();
            services.AddTransient<ICategoryTransferConverter, CategoryTransferConverter>();
            services.AddTransient<ISizeTransferConverter, SizeTransferConverter>();
            services.AddTransient<ISizeGroupTransferConverter, SizeGroupTransferConverter>();
            services.AddTransient<IColorClothesTransferConverter, ColorClothesTransferConverter>();
            services.AddTransient<IClothesShortTransferConverter, ClothesShortTransferConverter>();
            services.AddTransient<IClothesTransferConverter, ClothesTransferConverter>();
        }

        /// <summary>
        ///  Внедрить зависимости конвертеров сущностей базы данных
        /// </summary>
        public static void InjectEntityConverters(IServiceCollection services)
        {
            services.AddTransient<IGenderEntityConverter, GenderEntityConverter>();
            services.AddTransient<IClothesTypeEntityConverter, ClothesTypeEntityConverter>();
            services.AddTransient<ICategoryEntityConverter, CategoryEntityConverter>();
            services.AddTransient<ISizeEntityConverter, SizeEntityConverter>();
            services.AddTransient<ISizeGroupEntityConverter, SizeGroupEntityConverter>();
            services.AddTransient<IColorClothesEntityConverter, ColorClothesEntityConverter>();
            services.AddTransient<IClothesShortEntityConverter, ClothesShortEntityConverter>();
            services.AddTransient<IClothesInformationEntityConverter, ClothesInformationEntityConverter>();
        }
    }
}