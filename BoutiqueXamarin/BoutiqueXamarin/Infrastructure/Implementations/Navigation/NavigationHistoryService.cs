﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Sync;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    /// <summary>
    /// Сервис хранения истории навигации
    /// </summary>
    public class NavigationHistoryService: INavigationHistoryService
    {
        /// <summary>
        /// История переходов
        /// </summary>
        public IReadOnlyCollection<BaseNavigationOptions> NavigationHistory { get; private set; } =
            new List<BaseNavigationOptions>();

        /// <summary>
        /// Добавить в очередь переходов
        /// </summary>
        public IReadOnlyCollection<BaseNavigationOptions> EnqueueHistory<TOption>(TOption options)
            where TOption : BaseNavigationOptions =>
            NavigationHistory.
            Append(options).
            ToList().
            Void(history => NavigationHistory = history);

        /// <summary>
        /// Изъять из очереди переходов
        /// </summary>
        public TOption DequeueHistory<TOption>()
            where TOption : BaseNavigationOptions =>
            GetHistoryOptions<TOption>(NavigationHistory).
            Void(history => NavigationHistory = history).
            Map(history => (TOption)history.Last());

        /// <summary>
        /// Получить остаток и параметр
        /// </summary>
        private static IReadOnlyCollection<BaseNavigationOptions> GetHistoryOptions<TOption>(IReadOnlyCollection<BaseNavigationOptions> navigationHistory)
            where TOption : BaseNavigationOptions =>
            navigationHistory.
            Except(GetHistoryTail<TOption>(navigationHistory)).
            ToList();

        /// <summary>
        /// Получить остаток истории до текущего
        /// </summary>
        private static IEnumerable<BaseNavigationOptions> GetHistoryTail<TOption>(IEnumerable<BaseNavigationOptions> navigationHistory)
            where TOption : BaseNavigationOptions =>
            navigationHistory.
            Reverse().
            TakeWhile(options => options.GetType() != typeof(TOption)).
            Reverse();
    }
}