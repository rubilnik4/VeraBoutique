using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Services.Clothes
{
    /// <summary>
    /// Тестовый Api сервис одежды
    /// </summary>
    public static class ClothesApiServiceMock
    {
        /// <summary>
        /// Получить Api сервиса одежды
        /// </summary>
        public static Mock<IClothesApiService> GetClothesApiService(IResultCollection<ClothesTransfer> clothes) =>
            new Mock<IClothesApiService>().
            Void(mock => mock.Setup(service => service.GetClothes(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothes));
    }
}