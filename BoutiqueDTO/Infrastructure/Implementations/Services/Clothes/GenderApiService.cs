using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Connection;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Api сервис типа пола
    /// </summary>
    public class GenderApiService : ApiServiceBase<GenderType, GenderTransfer>, IGenderApiService
    {
        public GenderApiService(IHostConnection hostConnection)
        {
            _client = new RestClient(hostConnection.Host) { Timeout = hostConnection.TimeOut * 1000 };
        }

        /// <summary>
        /// Api сервис. Тип пола
        /// </summary>
        private readonly RestClient _client;

        /// <summary>
        /// Получить данные Api
        /// </summary>
        protected override async Task<IReadOnlyCollection<GenderTransfer>> GetApi() =>
            await _client.GetAsync<List<GenderTransfer>>(new RestRequest("api/gender", DataFormat.Json));

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        protected override async Task<GenderTransfer> GetApi(GenderType id) =>
            throw new Exception();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        protected override async Task<GenderType> PostApi(GenderTransfer transfer) =>
             (await _client.ExecuteGetAsync<GenderType>(new RestRequest("api/gender").AddJsonBody(transfer))).;

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        protected override async Task<IReadOnlyCollection<GenderType>> PostCollectionApi(IEnumerable<GenderTransfer> transfers) =>
            throw new Exception();

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        protected override async Task PutApi(GenderTransfer transfer) =>
             throw new Exception();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        protected override async Task<GenderTransfer> DeleteApi(GenderType id) =>
            throw new Exception();
    }
}