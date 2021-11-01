namespace BoutiqueXamarin.Models.Implementations.Navigation.Base
{
    public abstract class AuthorizeBaseNavigationOptions: BaseNavigationOptions
    {
        protected AuthorizeBaseNavigationOptions(string token)
        {
            Token = token;
        }

        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; }
    }
}