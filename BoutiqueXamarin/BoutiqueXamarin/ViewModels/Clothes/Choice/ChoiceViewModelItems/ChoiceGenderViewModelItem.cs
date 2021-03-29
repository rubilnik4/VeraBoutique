using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice.ChoiceViewModelItems
{
    /// <summary>
    /// Тип одежды
    /// </summary>
    public class ChoiceGenderViewModelItem
    {
        public ChoiceGenderViewModelItem(IGenderCategoryDomain genderCategory)
        {
            _genderCategory = genderCategory;
            ChoiceBaseViewModelItems = ToChoiceCategoryItems(genderCategory);
        }

        /// <summary>
        /// Пол
        /// </summary>
        private readonly IGenderCategoryDomain _genderCategory;

        /// <summary>
        /// Наименование типа пола
        /// </summary>
        public string GenderName => _genderCategory.Name;

        /// <summary>
        /// Модели выбора одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceBaseViewModelItem> ChoiceBaseViewModelItems { get; }

        /// <summary>
        /// Преобразовать в модель категорий одежды
        /// </summary>
        public static IReadOnlyCollection<ChoiceBaseViewModelItem> ToChoiceCategoryItems(IGenderCategoryDomain genderCategory) =>
            genderCategory.Categories.
            SelectMany(category => new List<ChoiceBaseViewModelItem> { new ChoiceCategoryViewModelItem(category) }.
                                   Concat(ChoiceCategoryViewModelItem.ToChoiceClothesTypeItems(category))).
            ToList();
    }
}