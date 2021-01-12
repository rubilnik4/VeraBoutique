using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.Connection;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис группы размеров одежды
    /// </summary>
    public class SizeGroupApiService : ApiService<(ClothesSizeType, int), SizeGroupTransfer>, ISizeGroupApiService
    {
        public SizeGroupApiService(IRestClient restClient)
          : base(restClient)
        { }

        /// <summary>
        /// Получить сервис
        /// </summary>
        public static ISizeGroupApiService GetSizeGroupApiService(IHostConnection hostConnection) =>
            new SizeGroupApiService(RestSharpFactory.GetRestClient(hostConnection));
    }
}