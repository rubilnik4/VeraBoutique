using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoutiqueXamarin.ViewModels.Base;

namespace BoutiqueXamarin.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        { }

        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Главное меню";
    }
}
