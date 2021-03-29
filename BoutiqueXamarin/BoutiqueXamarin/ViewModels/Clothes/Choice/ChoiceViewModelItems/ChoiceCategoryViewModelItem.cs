using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice.ChoiceViewModelItems
{
    /// <summary>
    /// Категория одежды
    /// </summary>
    public class ChoiceCategoryViewModelItem: ChoiceBaseViewModelItem
    {
        public ChoiceCategoryViewModelItem(ICategoryClothesTypeDomain categoryClothesType)
        {
            _categoryClothesType = categoryClothesType;
        }

        /// <summary>
        /// Категория одежды с подтипами
        /// </summary>
        private readonly ICategoryClothesTypeDomain _categoryClothesType;

        /// <summary>
        /// Категория
        /// </summary>
        public string CategoryName =>
            _categoryClothesType.Name;

        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<ChoiceClothesTypeViewModelItem> ToChoiceClothesTypeItems(ICategoryClothesTypeDomain category) =>
            category.ClothesTypes.
            Select(clothesType => new ChoiceClothesTypeViewModelItem(clothesType)).
            ToList();
    }
}