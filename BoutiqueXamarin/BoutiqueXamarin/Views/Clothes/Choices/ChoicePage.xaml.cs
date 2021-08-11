using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using Functional.FunctionalExtensions.Sync;
using ReactiveUI;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoicePage : ChoicePageBase
    {
        public ChoicePage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.ViewModel!.ErrorConnectionViewModel).
                     WhereNotNull().
                     Select(x => x.ResultError.HasErrors).
                     BindTo(this, x => x.ErrorView.IsVisible).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.ErrorConnectionViewModel).
                     WhereNotNull().
                     Select(x => x.ResultError.OkStatus).
                     BindTo(this, x => x.MainStackLayout.IsVisible).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.ChoiceGenderViewModelItems).
                     Select(items => (IList)items).
                     BindTo(this, x => x.GenderTab.TabItemsSource).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.ChoiceGenderViewModelItems, x => x.CategoryCarouselView.ItemsSource).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.SelectedGenderViewModelItem, x => x.GenderTab.SelectedIndex,
                          choiceGender => GetGenderViewModelItemIndex(choiceGender, ViewModel!.ChoiceGenderViewModelItems),
                          index => GetGenderViewModelItemByIndex(index, ViewModel!.ChoiceGenderViewModelItems)).
                     DisposeWith(disposable);

                this.Bind(ViewModel, x => x.SelectedGenderViewModelItem, x => x.CategoryCarouselView.CurrentItem).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Получить индекс типа пола
        /// </summary>
        private static int GetGenderViewModelItemIndex(ChoiceGenderViewModelItem? choiceGenderViewModelItem,
                                                       IList<ChoiceGenderViewModelItem>? choiceGenderViewModelItems) =>
            choiceGenderViewModelItem != null 
                ? choiceGenderViewModelItems?.IndexOf(choiceGenderViewModelItem) ?? -1
                : -1;

        /// <summary>
        /// Получить типа пола пол индексу
        /// </summary>
        private static ChoiceGenderViewModelItem? GetGenderViewModelItemByIndex(int index, IList<ChoiceGenderViewModelItem>? choiceGenderViewModelItems) =>
           index >= 0 && choiceGenderViewModelItems != null  && choiceGenderViewModelItems.Count > index
               ? choiceGenderViewModelItems[index]
               : null;
    }
}