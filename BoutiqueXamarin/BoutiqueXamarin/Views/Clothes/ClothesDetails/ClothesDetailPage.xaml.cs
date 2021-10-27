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
using BoutiqueXamarin.Views.ContentViews;
using BoutiqueXamarin.Views.ContentViews.MenuItems;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Calculate;

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

                this.OneWayBind(ViewModel, x => x.ClothesDetailDescriptionViewModel, x => x.ClothesDetailDescriptionView.ViewModel).
                     DisposeWith(disposable);

                this.WhenAnyValue(x => x.ScrollClothes.Height).
                     Where(heightRequest => heightRequest > 0).
                     Subscribe(heightRequest => CarouselImages.HeightRequest = heightRequest * 2 / 3).
                     DisposeWith(disposable);
            });
        }

        /// <summary>
        /// Главное окно
        /// </summary>
        protected override BackLeftMenuView BackLeftMenuView =>
            this.BackLeftMenu;

        /// <summary>
        /// Меню навигации назад
        /// </summary>
        protected override UserRightMenuView UserRightMenuView =>
            this.UserRightMenu;
    }
}