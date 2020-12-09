using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды. Трансферная модель
    /// </summary>
    public interface ISizeTransfer : ISizeBase, ITransferModel<(SizeType, string)>
    { }
}