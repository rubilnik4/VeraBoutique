using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarin.Models.Interfaces;
using BoutiqueXamarin.Models.Interfaces.Configuration;
using BoutiqueXamarin.ViewModels.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.Models.Interfaces.Result;
using Prism.Commands;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : ViewModelBase
    {
        public ChoiceViewModel(INavigationService navigationService, IBoutiqueProject boutiqueProject,
                               IXamarinConfigurationDomain xamarinConfigurationDomain)
          : base(navigationService)
        {
            ChoiceViewModelItems = new ObservableCollection<ChoiceViewModelItem>(boutiqueProject.Genders.Select(gender => new ChoiceViewModelItem(gender)));
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Выбор категории";

        /// <summary>
        /// Типы пола
        /// </summary>
        public ObservableCollection<ChoiceViewModelItem> ChoiceViewModelItems { get; }
    }
}