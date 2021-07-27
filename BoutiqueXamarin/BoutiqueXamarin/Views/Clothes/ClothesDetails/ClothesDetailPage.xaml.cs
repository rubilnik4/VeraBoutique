using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems;
using ReactiveUI;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace BoutiqueXamarin.Views.Clothes.ClothesDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClothesDetailPage : ClothesDetailPageBase
    {
        public ClothesDetailPage()
        {
            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, x => x.ClothesDetailImageViewModelItems, x => x.CarouselImages.ItemsSource).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Name, x => x.NameLabel.Text).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Price, x => x.PriceLabel.Text).
                     DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.Description, x => x.DescriptionLabel.Text).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.Sizes).
                     WhereNotNull().
                     Select(items => (IList)items).
                     BindTo(this, x => x.SizePicker.ItemsSource).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ViewModel!.Colors).
                     WhereNotNull().
                     Select(items => (IList)items).
                     BindTo(this, x => x.ColorPicker.ItemsSource).
                     DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.NavigateBackCommand, x => x.BackButton).
                     DisposeWith(disposable);
            });
        }
    }
}