using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис типа одежды
    /// </summary>
    public class ClothesTypeApiService : ApiService<string, ClothesTypeMainTransfer>, IClothesTypeApiService
    {
        public ClothesTypeApiService(IRestClient restClient)
            : base(restClient)
        { }
    }
}