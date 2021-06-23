using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Views.Clothes.Choices;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesPage : ClothesPageBase
    {
        public ClothesPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.ClothesViewModelColumnItems, x => x.ClothesColumns.ItemsSource).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.FilterButton.IsChecked).
                     BindTo(this, x => x.FilterView.IsVisible).
                     DisposeWith(disposable);
            });
        }
    }
}