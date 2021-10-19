using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Akavache;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize
{
    /// <summary>
    /// Хранение и загрузка данных аутентификации
    /// </summary>
    public class LoginStore: ILoginStore
    {
        /// <summary>
        /// 
        /// </summary>
        private const string TOKEN_KEY = "jwtToken";

        /// <summary>
        /// Сохранить токен
        /// </summary>
        public async Task<IResultError> SaveToken(string token) =>
             await token.ToResultValueWhere(_ => !String.IsNullOrWhiteSpace(token),
                                            _ => TokenErrorType).
             ResultValueOkAsync(_ => BlobCache.Secure.InsertObject(TOKEN_KEY, token).ToTask());

        /// <summary>
        /// Получить токен
        /// </summary>
        public async Task<IResultValue<string>> GetToken() =>
            await BlobCache.Secure.GetAllKeys().
            ToTask().
            ToResultValueWhereTaskAsync(keys => keys.Contains(TOKEN_KEY),
                                    _ => TokenErrorType).
            ResultValueOkBindAsync(_ => BlobCache.Secure.GetObject<string>(TOKEN_KEY).ToTask()).
            ResultValueBindOkTaskAsync(token => token.ToResultValueNullCheck(TokenErrorType)).
            ResultValueBindOkTaskAsync(token => token.ToResultValueWhere(_ => !String.IsNullOrWhiteSpace(token),
                                                                         _ => TokenErrorType));

        /// <summary>
        /// Очистить токен
        /// </summary>
        public async Task ClearToken() =>
             await BlobCache.Secure.Invalidate(TOKEN_KEY);

        /// <summary>
        /// Ошибка получения токена
        /// </summary>
        private static IErrorResult TokenErrorType =>
           ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Token, "Токен не задан");
    }
}