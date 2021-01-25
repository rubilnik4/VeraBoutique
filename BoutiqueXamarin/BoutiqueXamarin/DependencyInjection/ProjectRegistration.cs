using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Models.Implementation;
using BoutiqueXamarin.Models.Interfaces;
using Prism.Ioc;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация моделей проекта
    /// </summary>
    public static class ProjectRegistration
    {
        /// <summary>
        /// Регистрация моделей проекта
        /// </summary>
        public static void RegisterProject(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IBoutiqueProject, BoutiqueProject>();
        }
    }
}