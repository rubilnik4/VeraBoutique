using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using BoutiqueXamarin.Views.ContentViews;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using ResultFunctional.FunctionalExtensions.Sync;
using ReactiveUI;
using ReactiveUI.XamForms;
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
                this.WhenAnyValue(x => x.ViewModel!.ChoiceGenderViewModelItems).
                     WhereNotNull().
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
        /// Главное окно
        /// </summary>
        protected override ContentView MainContentView =>
            this.MainView;

        /// <summary>
        /// Окно ошибок
        /// </summary>
        protected override ErrorConnectionView ErrorContentView =>
            this.ErrorView;

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        protected override UserRightMenuView UserRightMenuView =>
            UserRightMenu;

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
           index >= 0 && choiceGenderViewModelItems != null && choiceGenderViewModelItems.Count > index
               ? choiceGenderViewModelItems[index]
               : null;
    }
}