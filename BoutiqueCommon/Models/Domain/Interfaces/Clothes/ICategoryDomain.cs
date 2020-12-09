using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Категория одежды. Доменная модель
    /// </summary>
    public interface ICategoryDomain : ICategoryBase, IDomainModel<string>
    { }
}