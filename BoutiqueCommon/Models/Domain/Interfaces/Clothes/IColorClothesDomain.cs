using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Цвет одежды. Доменная модель
    /// </summary>
    public interface IColorClothesDomain : IColorClothes, IDomainModel<string>
    { }
}