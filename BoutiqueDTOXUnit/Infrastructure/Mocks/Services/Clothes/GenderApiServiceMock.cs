using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Services.Api;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Services.Clothes
{
    /// <summary>
    /// Тестовый Api сервис
    /// </summary>
    public static class GenderApiServiceMock
    {
        /// <summary>
        /// Получить Api сервис типа пола
        /// </summary>
        public static Mock<IGenderApiService> GetGenderApiServiceGet(IResultCollection<GenderCategoryTransfer> genders) =>
            new Mock<IGenderApiService>().
            Void(mock => mock.Setup(service => service.GetGenderCategories()).
                              Returns(genders)).
            Void(mock => mock.Setup(service => service.GetGenderCategoriesAsync()).
                              ReturnsAsync(genders));
}