using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.Views.Base;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace BoutiqueXamarin.Views.Clothes.Choices
{
    /// <summary>
    /// Базовый класс страницы выбора
    /// </summary>
    public abstract class ChoicePageBase : NavigationLoginContentPage<ChoiceViewModel, ChoiceNavigationParameters, IChoiceNavigationService>
    { }
}