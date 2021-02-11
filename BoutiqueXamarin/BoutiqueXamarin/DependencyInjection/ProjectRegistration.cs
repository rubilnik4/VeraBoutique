using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarinCommon.Models.Implementation;
using BoutiqueXamarinCommon.Models.Interfaces;
using Prism.Ioc;
using Prism.Unity;
using Prism.Container.Extensions;

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
        public static void RegisterProject(IBoutiqueContainer container)
        {
            container.RegisterSingleton<IBoutiqueProject, BoutiqueProject>();
        }
    }
}