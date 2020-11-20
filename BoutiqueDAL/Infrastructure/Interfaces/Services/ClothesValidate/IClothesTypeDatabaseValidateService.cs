using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы типов одежды
    /// </summary>
    public interface IClothesTypeDatabaseValidateService : IDatabaseValidateService<string, IClothesTypeDomain>
    { }
}