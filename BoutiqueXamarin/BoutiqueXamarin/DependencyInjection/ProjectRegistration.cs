using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarin.Models.Implementation;
using BoutiqueXamarin.Models.Interfaces;
using BoutiqueXamarin.Models.Interfaces.Configuration;
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
            containerRegistry.RegisterSingleton<IXamarinConfigurationDomain>(
                service => service.Resolve<IXamarinConfigurationManager>().GetConfiguration());

            containerRegistry.RegisterSingleton<IBoutiqueProject, BoutiqueProject>();
        }
    }
}