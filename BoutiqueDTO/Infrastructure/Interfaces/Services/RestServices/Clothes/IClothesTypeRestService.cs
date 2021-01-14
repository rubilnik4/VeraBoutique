using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис загрузки типа одежды в базу данных
    /// </summary>
    public interface IClothesTypeRestService : IRestServiceBase<string, IClothesTypeDomain>
    { }
}