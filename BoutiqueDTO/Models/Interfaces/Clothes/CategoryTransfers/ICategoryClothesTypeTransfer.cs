using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes.CategoryTransfers
{
    /// <summary>
    /// Категория одежды с подтипами. Трансферная модель
    /// </summary>
    public interface ICategoryClothesTypeTransfer: ICategoryClothesTypeBase<ClothesTypeTransfer>, ICategoryTransfer
    { }
}