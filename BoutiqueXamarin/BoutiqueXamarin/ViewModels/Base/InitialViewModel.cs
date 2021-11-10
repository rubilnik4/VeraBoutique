using System;
using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using Prism.Navigation;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Стартовая страница
    /// </summary>
    public class InitialViewModel : BaseViewModel
    {
        public InitialViewModel(IClothesNavigationService clothesNavigationService)
        {
           Observable.FromAsync(clothesNavigationService.ToChoicePage).
                      Subscribe();
        }
    }
}