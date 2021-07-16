using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels;
using ReactiveUI;
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
                this.OneWayBind(ViewModel, x => x.ChoiceGenderViewModelItems, x => x.ItemsSource).
                     DisposeWith(disposable);
            });
        }
    }
}