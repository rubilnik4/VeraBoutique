using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels.ChoiceViewModelItems
{
    /// <summary>
    /// Категория одежды
    /// </summary>
    public class ChoiceCategoryViewModelItem : ChoiceBaseViewModelItem
    {
        public ChoiceCategoryViewModelItem(ICategoryClothesTypeDomain categoryClothesType)
        {
            _categoryClothesType = categoryClothesType;
            ChoiceClothesTypeViewModelItems = ToChoiceClothesTypeItems(categoryClothesType);
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
        /// МОдели типов одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceClothesTypeViewModelItem> ChoiceClothesTypeViewModelItems { get; }

        /// <summary>
        /// Видимость типов одежды
        /// </summary>
        private bool _clothesTypesVisible;

        /// <summary>
        /// Видимость типов одежды
        /// </summary>
        public bool ClothesTypesVisible 
        { 
            get => _clothesTypesVisible; 
            private set => this.RaiseAndSetIfChanged(ref _clothesTypesVisible, value);
        }

        /// <summary>
        /// Изменить видимость типов одежды
        /// </summary>
        public void ClothesTypesVisibleChange() =>
            ClothesTypesVisible = !ClothesTypesVisible;

        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<ChoiceClothesTypeViewModelItem> ToChoiceClothesTypeItems(ICategoryClothesTypeDomain category) =>
            category.ClothesTypes.
            Select(clothesType => new ChoiceClothesTypeViewModelItem(clothesType)).
            ToList();
    }
}