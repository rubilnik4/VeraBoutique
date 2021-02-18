using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис одежды
    /// </summary>
    public interface IClothesApiService : IApiService<int, ClothesMainTransfer>
    { }
}