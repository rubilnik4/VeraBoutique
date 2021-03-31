using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services.Clothes;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис одежды. Тесты
    /// </summary>
    public class ClothesRestServiceTest
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetClothesAsync_Ok()
        {
            var clothes = ClothesTransfersData.ClothesTransfers;
            var resultClothes = new ResultCollection<ClothesTransfer>(clothes);
            var clothesApiService = ClothesApiServiceMock.GetClothesApiService(resultClothes);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;
            var clothesMainTransferConverter = ClothesTransferConverterMock.ClothesMainTransferConverter;
            var clothesRestService = new ClothesRestService(clothesApiService.Object, clothesTransferConverter,
                                                            clothesMainTransferConverter);

            var result = await clothesRestService.GetClothesAsync(clothes.First().GenderType, clothes.First().ClothesTypeName);
            var clothesDomains = clothesTransferConverter.FromTransfers(clothes);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.SequenceEqual(clothesDomains.Value));
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetClothesAsync_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var resultClothes = new ResultCollection<ClothesTransfer>(error);
            var clothesApiService = ClothesApiServiceMock.GetClothesApiService(resultClothes);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;
            var clothesMainTransferConverter = ClothesTransferConverterMock.ClothesMainTransferConverter;
            var clothesRestService = new ClothesRestService(clothesApiService.Object, clothesTransferConverter,
                                                            clothesMainTransferConverter);

            var result = await clothesRestService.GetClothesAsync(GenderType.Male, String.Empty);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }
    }
}