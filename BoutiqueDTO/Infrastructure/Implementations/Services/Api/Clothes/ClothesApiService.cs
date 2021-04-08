using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис одежды
    /// </summary>
    public class ClothesApiService : ApiService<int, ClothesMainTransfer>, IClothesApiService
    {
        public ClothesApiService(HttpClient httpClient)
            : base(httpClient)
        { }

        /// <summary>
        /// Получить одежду по типу пола и категории
        /// </summary>
        public async Task<IResultCollection<ClothesTransfer>> GetClothes(GenderType genderType, string clothesType) =>
            await new List<string> { genderType.ToString(), clothesType}.
            Map(parameters => ApiRestRequest.GetRequest(ControllerName, parameters)).
            MapAsync(request => HttpClient.GetAsync(request)).
            ToRestResultCollectionTaskAsync<ClothesTransfer>();

        /// <summary>
        /// Получить изображение одежды
        /// </summary>
        public async Task<IResultValue<byte[]>> GetImage(int clothesId) =>
            await ApiRestRequest.GetRequest(clothesId, ControllerName, ClothesRoutes.IMAGE_ROUTE).
            MapAsync(request => HttpClient.GetAsync(request)).
            ToRestResultValueTaskAsync<byte[]>();
    }
}