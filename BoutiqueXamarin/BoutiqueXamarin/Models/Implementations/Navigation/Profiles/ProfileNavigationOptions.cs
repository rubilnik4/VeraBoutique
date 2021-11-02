using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Profiles
{
    /// <summary>
    /// Параметры навигации к странице информации о пользователе
    /// </summary>
    public class ProfileNavigationOptions : BaseNavigationOptions
    {
        public ProfileNavigationOptions(IBoutiqueUserDomain user)
        {
            User = user;
        }

        /// <summary>
        /// Пользователь
        /// </summary>
        public IBoutiqueUserDomain User { get; }
    }
}