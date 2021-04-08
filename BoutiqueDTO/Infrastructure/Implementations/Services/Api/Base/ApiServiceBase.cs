using System.Net.Http;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base
{
    /// <summary>
    /// Базовый сервис работы с api
    /// </summary>
    public abstract class ApiServiceBase<TId, TTransfer>: IApiServiceBase
        where TTransfer : ITransferModel<TId>
        where TId : notnull
    {
        protected ApiServiceBase(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        /// Клиент для Api сервисов
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <summary>
        /// Наименование контроллера
        /// </summary>
        public string ControllerName =>
            GetType().Name.SubstringRemove(typeof(ApiService<TId, TTransfer>).GetNameWithoutGenerics());
    }
}