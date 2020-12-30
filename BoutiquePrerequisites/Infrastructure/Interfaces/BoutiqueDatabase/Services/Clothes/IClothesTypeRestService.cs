using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Base;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes
{
    /// <summary>
    /// Сервис загрузки типа одежды в базу данных
    /// </summary>
    public interface IClothesTypeRestService : IRestServiceBase<string, IClothesTypeDomain>
    { }
}