using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды. Доменная модель
    /// </summary>
    public interface IClothesSizeDomain : ISize, IDomainModel<string>
    { }
}