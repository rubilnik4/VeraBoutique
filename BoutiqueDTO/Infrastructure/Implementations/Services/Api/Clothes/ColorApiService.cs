using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис цвета одежды
    /// </summary>
    public class ColorApiService : ApiService<string, ColorTransfer>, IColorApiService
    {
        public ColorApiService(IRestClient restClient)
           : base(restClient)
        { }
    }
}