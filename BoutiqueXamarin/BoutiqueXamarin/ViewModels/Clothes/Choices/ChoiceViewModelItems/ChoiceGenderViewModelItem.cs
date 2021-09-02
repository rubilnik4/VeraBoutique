using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using DynamicData;
using DynamicData.Binding;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems
{
    /// <summary>
    /// Тип одежды
    /// </summary>
    public class ChoiceGenderViewModelItem : BaseViewModel
    {
        public ChoiceGenderViewModelItem(IClothesNavigationService clothesNavigationService, IGenderCategoryDomain genderCategory)
        {
            _clothesNavigationService = clothesNavigationService;
            _genderCategory = genderCategory;

            var choiceCategoryViewModelItems = new ObservableCollection<ChoiceCategoryViewModelItem>(ToChoiceCategoryItems(_genderCategory));
            _choiceBaseViewModelItems = choiceCategoryViewModelItems.
                                        ToObservableChangeSet().
                                        AutoRefresh(choiceCategory => choiceCategory.ClothesTypesVisible).
                                        ToCollection().
                                        Select(ChoiceBaseViewModelItemsUpdate).
                                        ToProperty(this, nameof(ChoiceBaseViewModelItems));

            ChoiceBaseTapCommand = ReactiveCommand.CreateFromTask<ChoiceBaseViewModelItem, ChoiceBaseViewModelItem>(ChoiceBaseTapUpdate);
        }

        /// <summary>
        /// Сервис навигации к странице одежды
        /// </summary>
        private readonly IClothesNavigationService _clothesNavigationService;

        /// <summary>
        /// Пол
        /// </summary>
        private readonly IGenderCategoryDomain _genderCategory;

        /// <summary>
        /// Наименование типа пола
        /// </summary>
        public string GenderName => _genderCategory.Name;

        /// <summary>
        /// Список категорий одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ChoiceBaseViewModelItem>> _choiceBaseViewModelItems;

        /// <summary>
        /// Список категорий одежды
        /// </summary>
        public IReadOnlyCollection<ChoiceBaseViewModelItem> ChoiceBaseViewModelItems =>
            _choiceBaseViewModelItems.Value;

        /// <summary>
        /// Команда нажатия категории одежды
        /// </summary>
        public ReactiveCommand<ChoiceBaseViewModelItem, ChoiceBaseViewModelItem> ChoiceBaseTapCommand { get; }

        /// <summary>
        /// Получить список категорий одежды
        /// </summary>
        private static IReadOnlyCollection<ChoiceBaseViewModelItem> ChoiceBaseViewModelItemsUpdate(IReadOnlyCollection<ChoiceCategoryViewModelItem> choiceCategoryViewModelItems) =>
            choiceCategoryViewModelItems.
            SelectMany(choiceCategory =>
                Enumerable.Empty<ChoiceBaseViewModelItem>().
                Append(choiceCategory).
                WhereOk(_ => choiceCategory.ClothesTypesVisible,
                        choiceCategories => choiceCategories.Concat(choiceCategory.ChoiceClothesTypeViewModelItems))).
            ToList();

        /// <summary>
        /// Преобразовать в модель категорий одежды
        /// </summary>
        private static IReadOnlyCollection<ChoiceCategoryViewModelItem> ToChoiceCategoryItems(IGenderCategoryDomain genderCategory) =>
            genderCategory.Categories.
            Select(category => new ChoiceCategoryViewModelItem(category)).
            ToList();

        /// <summary>
        /// Нажатие на категорию одежды
        /// </summary>
        private async Task<ChoiceBaseViewModelItem> ChoiceBaseTapUpdate(ChoiceBaseViewModelItem choiceBaseViewModelItem) =>
            choiceBaseViewModelItem switch
            {
                ChoiceCategoryViewModelItem choiceCategory =>
                   choiceCategory.
                   Void(_ => choiceCategory.ClothesTypesVisibleChange()),
                ChoiceClothesTypeViewModelItem choiceClothesType =>
                    await choiceClothesType.
                    VoidAsync(_ => _clothesNavigationService.NavigateTo(_genderCategory.GenderType, choiceClothesType.ClothesType)),
                _ => throw new ArgumentException(nameof(choiceBaseViewModelItem)),
            };
    }
}