using System.Reactive.Disposables;
using System.Reactive.Linq;
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
    public abstract class ChoicePageBase : BaseContentPage<ChoiceViewModel>
    { }
}