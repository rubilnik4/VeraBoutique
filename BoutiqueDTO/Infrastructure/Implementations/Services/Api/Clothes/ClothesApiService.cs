using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис одежды
    /// </summary>
    public class ClothesApiService : ApiService<int, ClothesFullTransfer>, IClothesApiService
    {
        public ClothesApiService(IRestClient restClient)
            : base(restClient)
        { }
    }
}