using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;

namespace BoutiqueDTO.Models.Interfaces.Clothes.GenderTransfers
{
    /// <summary>
    /// Тип пола с категориями одежды
    /// </summary>
    public interface IGenderCategoryTransfer: IGenderCategoryBase<CategoryClothesTypeTransfer, ClothesTypeTransfer>, IGenderTransfer
    { }
}