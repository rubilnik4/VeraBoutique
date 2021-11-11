using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Profiles
{
    /// <summary>
    /// Параметры навигации к странице личной информации
    /// </summary>
    public class PersonalNavigationOptions : BaseNavigationOptions
    {
        public PersonalNavigationOptions(IBoutiqueUserDomain user)
        {
            User = user;
        }

        /// <summary>
        /// Пользователь
        /// </summary>
        public IBoutiqueUserDomain User { get; }
    }
}