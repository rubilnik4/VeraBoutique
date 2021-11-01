using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails;
using BoutiqueXamarin.Views.Base;
using ReactiveUI.XamForms;

namespace BoutiqueXamarin.Views.Clothes.ClothesDetails
{
    /// <summary>
    /// Подробная информация об одежде
    /// </summary>
    public abstract class ClothesDetailPageBase : NavigationBaseContentPage<ClothesDetailViewModel, ClothesDetailNavigationOptions>
    { }
}