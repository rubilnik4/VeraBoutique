using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Категория одежды. Трансферная модель
    /// </summary>
    public interface ICategoryTransfer : ICategory, ITransferModel<string>
    { }
}