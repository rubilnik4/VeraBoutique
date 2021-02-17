using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.CategoryTransfers
{
    /// <summary>
    /// Категория одежды. Трансферная модель
    /// </summary>
    public interface ICategoryTransfer : ICategoryBase, ITransferModel<string>
    { }
}