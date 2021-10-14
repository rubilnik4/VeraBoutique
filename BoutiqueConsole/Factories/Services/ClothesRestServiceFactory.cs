using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueConsole.Factories.Services
{
    /// <summary>
    /// Сервисы одежды
    /// </summary>
    public static class ClothesRestServiceFactory
    {
        /// <summary>
        /// Получить сервис типа пола
        /// </summary>
        public static IGenderRestService GetGenderRestService(IRestHttpClient restHttpClient) =>
             new GenderRestService(restHttpClient, new GenderTransferConverter(),
                                   new ClothesTypeTransferConverter().
                                   Map(clothesTypeConverter => new CategoryClothesTypeTransferConverter(clothesTypeConverter)).
                                   Map(categoryConverter => new GenderCategoryTransferConverter(categoryConverter)));

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static ICategoryRestService GetCategoryRestService(IRestHttpClient restHttpClient) =>
             new CategoryRestService(restHttpClient, new CategoryMainTransferConverter(new GenderTransferConverter()));

        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static IColorRestService GetColorRestService(IRestHttpClient restHttpClient) =>
             new ColorRestService(restHttpClient, new ColorTransferConverter());

        /// <summary>
        /// Получить сервис типа одежды
        /// </summary>
        public static IClothesTypeRestService GetClothesTypeRestService(IRestHttpClient restHttpClient) =>
             new ClothesTypeRestService(restHttpClient,
                                        new ClothesTypeMainTransferConverter(new CategoryTransferConverter()));

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeRestService GetSizeRestService(IRestHttpClient restHttpClient) =>
             new SizeRestService(restHttpClient, new SizeTransferConverter());

        /// <summary>
        /// Получить сервис размера одежды
        /// </summary>
        public static ISizeGroupRestService GetSizeGroupRestService(IRestHttpClient restHttpClient) =>
             new SizeGroupRestService(restHttpClient,
                                      new SizeGroupMainTransferConverter(new SizeTransferConverter()));

        /// <summary>
        /// Получить сервис одежды
        /// </summary>
        public static IClothesRestService GetClothesRestService(IRestHttpClient restHttpClient) =>
             new ClothesRestService(restHttpClient,
                                    new ClothesTransferConverter(),
                                    new ClothesDetailTransferConverter(new ColorTransferConverter(),
                                                                      new SizeGroupMainTransferConverter(new SizeTransferConverter())),
                                    new ClothesMainTransferConverter(new ClothesImageTransferConverter(),
                                                                     new GenderTransferConverter(),
                                                                     new ClothesTypeTransferConverter(),
                                                                     new ColorTransferConverter(),
                                                                     new SizeGroupMainTransferConverter(new SizeTransferConverter())),
                                    new ClothesImageTransferConverter());
    }
}