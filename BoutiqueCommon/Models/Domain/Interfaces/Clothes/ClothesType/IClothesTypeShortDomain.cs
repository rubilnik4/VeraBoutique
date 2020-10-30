using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType
{
    /// <summary>
    /// Вид одежды. Основные данные. Доменная модель
    /// </summary>
    public interface IClothesTypeShortDomain : IClothesType, IDomainModel<string>
    { }
}