using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels.ChoiceViewModelItems;
using Functional.FunctionalExtensions.Sync;
using ReactiveUI;
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
                this.OneWayBind(ViewModel, x => x.ChoiceGenderViewModelItems, x => x.GenderViews.ItemsSource).
                     DisposeWith(disposable);

                //this.Bind(ViewModel, x => x.SelectedChoiceGenderViewModel, x => x.GenderViews.CurrentIndex).
                //     DisposeWith(disposable);

                //this.OneWayBind(ViewModel, x => x.SelectedChoiceGenderViewModel.ChoiceBaseViewModelItems, x => x.CategoryListView.ItemsSource).
                //     DisposeWith(disposable);

                //this.CategoryListView.Events().ItemTapped.
                //     Select(_ => CategoryListView.SelectedItem).
                //     InvokeCommand(this, x => x.ViewModel!.SelectedChoiceGenderViewModel.ChoiceBaseTapCommand).
                //     DisposeWith(disposable);
            });
        }
    }
}