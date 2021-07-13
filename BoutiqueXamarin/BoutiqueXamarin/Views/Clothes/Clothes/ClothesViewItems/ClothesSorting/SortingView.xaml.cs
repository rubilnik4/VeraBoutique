using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Models.Enums;
using BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesFilters;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesSorting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SortingView : SortingViewBase
    {
        public SortingView()
        {
            InitializeComponent();
         
            this.WhenActivated(disposable =>
            {
                this.Bind(ViewModel, x => x.IsNamingSorting, x => x.NamingRadio.IsChecked).
                     DisposeWith(disposable);
                this.Bind(ViewModel, x => x.IsPriceSorting, x => x.PriceRadio.IsChecked).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Событие закрытия формы
        /// </summary>
        public IObservable<EventArgs> SortingHideButtonClick =>
            SortingHide.Events().Clicked;
    }
}