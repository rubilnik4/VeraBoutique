using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы типов одежды
    /// </summary>
    public interface IClothesTypeDatabaseValidateService : IDatabaseValidateService<string, IClothesTypeMainDomain>
    { }
}