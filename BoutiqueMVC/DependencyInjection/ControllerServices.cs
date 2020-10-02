﻿using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueMVC.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей для контроллеров
    /// </summary>
    public static class ControllerServices
    {
        /// <summary>
        /// Внедрить зависимости к базе данных
        /// </summary>
        public static void InjectControllerServices(IServiceCollection services)
        {
            ConverterServices.InjectTransferConverters(services);

            services.AddTransient<IUserManagerBoutique, UserManagerBoutique>();
            services.AddTransient<ISignInManagerBoutique, SignInManagerBoutique>();
        }
    }
}