using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Profiles
{
    /// <summary>
    /// Параметры навигации к странице информации о пользователе
    /// </summary>
    public class ProfileNavigationOptions : AuthorizeBaseNavigationOptions
    {
        public ProfileNavigationOptions(string? token)
            : base(token)
        { }
    }
}