﻿using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorize
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