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
        protected ApiServiceBase(IRestClient restClient)
        {
            RestClient = restClient;
        }

        /// <summary>
        /// Клиент для Api сервисов
        /// </summary>
        protected IRestClient RestClient { get; }

        /// <summary>
        /// Наименование контроллера
        /// </summary>
        public string ControllerName =>
            GetType().Name.SubstringRemove(typeof(ApiService<TId, TTransfer>).GetNameWithoutGenerics());
    }
}