using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize
{
    /// <summary>
    /// Сервис авторизации и сохранения логина
    /// </summary>
    public class LoginService: ILoginService
    {
        public LoginService(IAuthorizeRestService authorizeRestService, ILoginStore loginStore)
        {
            _authorizeRestService = authorizeRestService;
            _loginStore = loginStore;
        }

        /// <summary>
        /// Сервис авторизации
        /// </summary>
        private readonly IAuthorizeRestService _authorizeRestService;

        /// <summary>
        /// Сервис авторизации
        /// </summary>
        private readonly ILoginStore _loginStore;

        /// <summary>
        /// Авторизоваться через токен JWT
        /// </summary>
        public async Task<IResultError> Login(IAuthorizeDomain authorize) =>
            await authorize.ToResultValue().
            ResultValueVoidOkAsync(_ => Logout()).
            ResultValueBindOkBindAsync(_ => _authorizeRestService.AuthorizeJwt(authorize)).
            ResultValueBindErrorsOkBindAsync(_loginStore.SaveToken);

        /// <summary>
        /// Выйти из профиля
        /// </summary>
        public async Task Logout() =>
            await _loginStore.ClearToken();
    }
}