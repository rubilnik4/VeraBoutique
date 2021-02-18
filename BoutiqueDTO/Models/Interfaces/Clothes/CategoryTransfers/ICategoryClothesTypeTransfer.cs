using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Models.Interfaces.Clothes.CategoryTransfers
{
    /// <summary>
    /// Категория одежды с подтипами. Трансферная модель
    /// </summary>
    public interface ICategoryClothesTypeTransfer: ICategoryClothesTypeBase<ClothesTypeTransfer>, ICategoryTransfer
    { }
}