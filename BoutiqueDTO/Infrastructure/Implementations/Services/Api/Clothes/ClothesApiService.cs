using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Routes.Clothes;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис одежды
    /// </summary>
    public class ClothesApiService : ApiService<int, ClothesMainTransfer>, IClothesApiService
    {
        public ClothesApiService(IRestClient restClient)
            : base(restClient)
        { }

        /// <summary>
        /// Получить одежду по типу пола и категории
        /// </summary>
        public async Task<IResultCollection<ClothesTransfer>> GetClothes(GenderType genderType, string clothesType) =>
            await new List<string> { genderType.ToString(), clothesType}.
            Map(parameters => ApiRestRequest.GetJsonRequest(ControllerName, parameters)).
            MapAsync(route => RestClient.ExecuteAsync<List<ClothesTransfer>>(route)).
            ToRestResultCollectionAsync();

        /// <summary>
        /// Получить изображение одежды
        /// </summary>
        public async Task<IResultValue<byte[]>> GetImage(int clothesId) =>
            await ApiRestRequest.GetJsonRequest(clothesId, ControllerName).
            MapAsync(route => RestClient.ExecuteAsync<byte[]>(route)).
            ToRestResultValueAsync();
    }
}