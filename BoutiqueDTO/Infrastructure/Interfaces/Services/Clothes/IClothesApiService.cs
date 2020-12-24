using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Api сервис одежды
    /// </summary>
    public interface IClothesApiService : IApiService<int, ClothesTransfer>
    { }
}