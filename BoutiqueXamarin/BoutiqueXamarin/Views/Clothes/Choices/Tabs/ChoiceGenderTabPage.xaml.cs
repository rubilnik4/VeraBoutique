using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Choices.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoiceGenderTabPage : ChoiceGenderTabPageBase
    {
        public ChoiceGenderTabPage()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel).
                 Where(x => x != null).
                 Do(SetTitle).
                 Subscribe();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.ChoiceBaseViewModelItems, x => x.CategoryListView.ItemsSource).
                     DisposeWith(disposable);
            });

            this.CategoryListView.Events().ItemTapped.
                 Select(args => this.CategoryListView.SelectedItem).
                 InvokeCommand(this, x => x.ViewModel!.ChoiceBaseTapCommand);
        }

        /// <summary>
        /// Название страницы
        /// </summary>
        private void SetTitle(ChoiceGenderViewModelItem? choiceGenderBaseViewModelItem)
        {
            Title = choiceGenderBaseViewModelItem?.GenderName;
        }
    }
}