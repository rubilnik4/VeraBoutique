using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
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
            services.AddTransient<IGenderTransferConverter>(serviceProvider => new GenderTransferConverter());
            services.AddTransient<IClothesTypeTransferConverter>(serviceProvider => new ClothesTypeTransferConverter());
            services.AddTransient<ICategoryTransferConverter>(serviceProvider => new CategoryTransferConverter());
        }

        /// <summary>
        ///  Внедрить зависимости конвертеров сущностей базы данных
        /// </summary>
        public static void InjectEntityConverters(IServiceCollection services)
        {
            services.AddTransient<IGenderEntityConverter>(serviceProvider => new GenderEntityConverter());
            services.AddTransient<IClothesTypeEntityConverter>(serviceProvider => new ClothesTypeEntityConverter());
            services.AddTransient<ICategoryEntityConverter>(serviceProvider => new CategoryEntityConverter());
        }
    }
}