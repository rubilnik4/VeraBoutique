using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ImageEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ImageEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;
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
            services.AddTransient<IGenderCategoryTransferConverter, GenderCategoryTransferConverter>();
            services.AddTransient<IClothesTypeTransferConverter, ClothesTypeTransferConverter>();
            services.AddTransient<IClothesTypeMainTransferConverter, ClothesTypeMainTransferConverter>();
            services.AddTransient<ICategoryTransferConverter, CategoryTransferConverter>();
            services.AddTransient<ICategoryMainTransferConverter, CategoryMainTransferConverter>();
            services.AddTransient<ICategoryClothesTypeTransferConverter, CategoryClothesTypeTransferConverter>();
            services.AddTransient<ISizeTransferConverter, SizeTransferConverter>();
            services.AddTransient<ISizeGroupTransferConverter, SizeGroupTransferConverter>();
            services.AddTransient<ISizeGroupMainTransferConverter, SizeGroupMainTransferConverter>();
            services.AddTransient<IColorTransferConverter, ColorTransferConverter>();
            services.AddTransient<IClothesImageTransferConverter, ClothesImageTransferConverter>();
            services.AddTransient<IClothesTransferConverter, ClothesTransferConverter>();
            services.AddTransient<IClothesDetailTransferConverter, ClothesDetailTransferConverter>();
            services.AddTransient<IClothesMainTransferConverter, ClothesMainTransferConverter>();
        }

        /// <summary>
        ///  Внедрить зависимости конвертеров сущностей базы данных
        /// </summary>
        public static void RegisterEntityConverters(IServiceCollection services)
        {
            services.AddTransient<IGenderEntityConverter, GenderEntityConverter>();
            services.AddTransient<IGenderCategoryEntityConverter, GenderCategoryEntityConverter>();
            services.AddTransient<IClothesTypeMainEntityConverter, ClothesTypeMainEntityConverter>();
            services.AddTransient<IClothesTypeEntityConverter, ClothesTypeEntityConverter>();
            services.AddTransient<ICategoryEntityConverter, CategoryEntityConverter>();
            services.AddTransient<ICategoryMainEntityConverter, CategoryMainEntityConverter>();
            services.AddTransient<ICategoryClothesTypeEntityConverter, CategoryClothesTypeEntityConverter>();
            services.AddTransient<ISizeEntityConverter, SizeEntityConverter>();
            services.AddTransient<ISizeGroupEntityConverter, SizeGroupEntityConverter>();
            services.AddTransient<ISizeGroupMainEntityConverter, SizeGroupMainEntityConverter>();
            services.AddTransient<IColorClothesEntityConverter, ColorClothesEntityConverter>();
            services.AddTransient<IClothesImageEntityConverter, ClothesImageEntityConverter>();
            services.AddTransient<IClothesEntityConverter, ClothesEntityConverter>();
            services.AddTransient<IClothesDetailEntityConverter, ClothesDetailEntityConverter>();
            services.AddTransient<IClothesMainEntityConverter, ClothesMainEntityConverter>();
        }
    }
}