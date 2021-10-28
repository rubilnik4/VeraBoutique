using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.Views.Base;
using ReactiveUI.XamForms;

namespace BoutiqueXamarin.Views.Clothes.Clothes
{
    /// <summary>
    /// Списки одежды
    /// </summary>
    public abstract class ClothesPageBase : NavigationBaseContentPage<ClothesViewModel, ClothesNavigationOptions, IClothesNavigationService>
    { }
}