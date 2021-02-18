using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;

namespace BoutiqueDTO.Models.Interfaces.Clothes.CategoryTransfers
{
    /// <summary>
    /// Категория одежды. Трансферная модель модель
    /// </summary>
    public interface ICategoryMainTransfer: ICategoryMainBase<GenderTransfer>, ICategoryTransfer
    { }
}