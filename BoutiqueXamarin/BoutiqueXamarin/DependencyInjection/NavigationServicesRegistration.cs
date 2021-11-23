using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Converters;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Converters;

namespace BoutiqueXamarin.DependencyInjection
{
    /// <summary>
    /// Регистрация сервисов навигации
    /// </summary>
    public static class NavigationServicesRegistration
    {
        /// <summary>
        /// Регистрация конвертеров трансферных моделей
        /// </summary>
        public static void RegisterNavigationServices(IBoutiqueContainer container)
        {
            container.RegisterSingleton<INavigationHistoryService, NavigationHistoryService>();
            container.Register<INavigationServiceFactory, NavigationServiceFactory>();
            container.Register<IClothesNavigationService, ClothesNavigationService>();
            container.Register<IProfileNavigationService, ProfileNavigationService>();
        }
    }
}