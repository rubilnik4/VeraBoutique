using System.Collections.Generic;
using System.Linq;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Sync;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    public interface INavigationHistoryService
    {
        /// <summary>
        /// История переходов
        /// </summary>
        IReadOnlyCollection<BaseNavigationOptions> NavigationHistory { get;  } 

        /// <summary>
        /// Добавить в очередь переходов
        /// </summary>
        IReadOnlyCollection<BaseNavigationOptions> EnqueueHistory<TOption>(TOption options)
            where TOption : BaseNavigationOptions;

        /// <summary>
        /// Изъять из очереди переходов
        /// </summary>
        TOption DequeueHistory<TOption>()
            where TOption : BaseNavigationOptions;
    }
}