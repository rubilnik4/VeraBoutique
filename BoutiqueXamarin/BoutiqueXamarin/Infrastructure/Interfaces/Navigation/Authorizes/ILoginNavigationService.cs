﻿using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes
{
    /// <summary>
    /// Сервис навигации к странице авторизации
    /// </summary>
    public interface ILoginNavigationService : IBaseNavigationService<LoginNavigationParameters>
    {
        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task NavigateTo();
    }
}