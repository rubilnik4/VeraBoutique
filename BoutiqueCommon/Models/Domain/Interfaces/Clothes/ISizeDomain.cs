using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды. Доменная модель
    /// </summary>
    public interface ISizeDomain : ISize, IDomainModel<(SizeType, string)>
    {
        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        string SizeNameShort { get; }
    }
}