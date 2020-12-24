using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Connection;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис размера одежды
    /// </summary>
    public class SizeApiService : ApiService<(SizeType, string), SizeTransfer>, ISizeApiService
    {
        public SizeApiService(IRestClient restClient)
            : base(restClient)
        { }

        /// <summary>
        /// Получить сервис
        /// </summary>
        public static ISizeApiService GetSizeApiService(IHostConnection hostConnection) =>
            new SizeApiService(RestSharpFactory.GetRestClient(hostConnection));
    }
}