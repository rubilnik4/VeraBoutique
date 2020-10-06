using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды. Трансферная модель
    /// </summary>
    public interface IClothesSizeTransfer : ISize, ITransferModel<string>
    { }
}