using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Navigation;
using BoutiqueXamarin.Views.Clothes.Clothes;
using Functional.FunctionalExtensions.Sync;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems
{
    /// <summary>
    /// Тип одежды
    /// </summary>
    public class ChoiceGenderViewModelItem : ViewModelBase
    {
        public ChoiceGenderViewModelItem(INavigationService navigationService, IGenderCategoryDomain genderCategory)
            : base(navigationService)
        {
            _genderCategory = genderCategory;
            _choiceCategoryViewModelItems = ToChoiceCategoryItems(genderCategory);
            ChoiceBaseTapCommand = new DelegateCommand<ChoiceBaseViewModelItem>(async choice => await ChoiceBaseTapUpdate(choice));
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
        /// Модели категорий одежды
        /// </summary>
        private readonly IReadOnlyCollection<ChoiceCategoryViewModelItem> _choiceCategoryViewModelItems;

        /// <summary>
        /// Модели выбора категории одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceBaseViewModelItem> ChoiceBaseViewModelItems =>
            _choiceCategoryViewModelItems.
            SelectMany(choiceCategory =>
                Enumerable.Empty<ChoiceBaseViewModelItem>().
                Append(choiceCategory).
                WhereOk(_ => choiceCategory.ClothesTypesVisible,
                        choiceCategories => choiceCategories.Concat(choiceCategory.ChoiceClothesTypeViewModelItems))).
            ToList();

        /// <summary>
        /// Выбранная модель категории
        /// </summary>
        private ChoiceBaseViewModelItem? _selectedChoiceBaseViewModelItem;

        /// <summary>
        /// Выбранная модель категории
        /// </summary>
        public ChoiceBaseViewModelItem? SelectedChoiceBaseViewModelItem
        {
            get => _selectedChoiceBaseViewModelItem;
            set => SetProperty(ref _selectedChoiceBaseViewModelItem, value);
        }

        /// <summary>
        /// Команда нажатия категории одежды
        /// </summary>
        public DelegateCommand<ChoiceBaseViewModelItem> ChoiceBaseTapCommand { get; }

        /// <summary>
        /// Нажатие на категорию одежды
        /// </summary>
        private async Task ChoiceBaseTapUpdate(ChoiceBaseViewModelItem choiceBaseViewModelItem)
        {
            switch (choiceBaseViewModelItem)
            {
                case ChoiceCategoryViewModelItem choiceCategory:
                    ChoiceItemsUpdate(choiceCategory);
                    break;
                case ChoiceClothesTypeViewModelItem choiceClothesType:
                    await ClothesNavigation.ToClothesPage(NavigationService, choiceClothesType.ClothesTypeName);
                    break;
            }
        }

        /// <summary>
        /// Обновление списка категорий одежды
        /// </summary>
        private void ChoiceItemsUpdate(ChoiceCategoryViewModelItem choiceCategory) =>
            choiceCategory.
            Void(_ => choiceCategory.ClothesTypesVisibleChange()).
            Void(_ => RaisePropertyChanged(nameof(ChoiceBaseViewModelItems)));

        /// <summary>
        /// Преобразовать в модель категорий одежды
        /// </summary>
        public static IReadOnlyCollection<ChoiceCategoryViewModelItem> ToChoiceCategoryItems(IGenderCategoryDomain genderCategory) =>
            genderCategory.Categories.
            Select(category => new ChoiceCategoryViewModelItem(category)).
            ToList();
    }
}