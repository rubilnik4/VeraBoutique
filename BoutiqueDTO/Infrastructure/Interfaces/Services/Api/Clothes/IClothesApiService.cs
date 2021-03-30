using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис одежды
    /// </summary>
    public interface IClothesApiService : IApiService<int, ClothesMainTransfer>
    {
        /// <summary>
        /// Получить одежду по типу пола и категории
        /// </summary>
        Task<IResultCollection<ClothesTransfer>> GetClothes(GenderType genderType, string clothesType);
    }
}