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
    public class InitialViewModel: BaseViewModel
    {
        public InitialViewModel(IClothesNavigationService clothesNavigationService)
        {
            _navigate = Observable.FromAsync(clothesNavigationService.ToChoicePage, scheduler: RxApp.MainThreadScheduler).
                                   ToProperty(this, nameof(Navigate));
        }

        private readonly ObservableAsPropertyHelper<INavigationResult> _navigate;

        public INavigationResult Navigate =>
            _navigate.Value;
    }
}