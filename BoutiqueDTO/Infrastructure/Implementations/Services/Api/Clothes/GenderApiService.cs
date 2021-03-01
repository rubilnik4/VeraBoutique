using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueDTO.Routes.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис типа пола
    /// </summary>
    public class GenderApiService : ApiService<GenderType, GenderTransfer>, IGenderApiService
    {
        public GenderApiService(IRestClient restClient)
            : base(restClient)
        { }

        /// <summary>
        /// Получить данные типа пола с категорией
        /// </summary>
        public IResultCollection<GenderCategoryTransfer> GetGenderCategories() =>
            RestClient.Execute<List<GenderCategoryTransfer>>(ApiRestRequest.GetJsonRequest(ControllerName,
                                                                                           GenderRoutes.GENDER_CATEGORY_ROUTE)).
            ToRestResultCollection();

        /// <summary>
        /// Получить данные типа пола с категорией асинхронно
        /// </summary>
        public async  Task<IResultCollection<GenderCategoryTransfer>> GetGenderCategoriesAsync() =>
            await RestClient.ExecuteAsync<List<GenderCategoryTransfer>>(ApiRestRequest.GetJsonRequest(ControllerName,
                                                                                                      GenderRoutes.GENDER_CATEGORY_ROUTE)).
            ToRestResultCollectionAsync();
    }
}