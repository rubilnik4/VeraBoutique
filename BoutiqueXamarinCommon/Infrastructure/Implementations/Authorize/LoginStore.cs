using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Akavache;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize
{
    /// <summary>
    /// Хранение и загрузка данных аутентификации
    /// </summary>
    public static class LoginStore
    {
        /// <summary>
        /// 
        /// </summary>
        private const string TOKEN_KEY = "jwtToken";

        /// <summary>
        /// Сохранить токен
        /// </summary>
        public static async Task<IResultError> SaveToken(string tokenInit) =>
             await tokenInit.ToResultValueWhere(token => !String.IsNullOrWhiteSpace(token),
                                                _ => TokenError).
             ResultValueOkAsync(async token => await BlobCache.Secure.InsertObject(TOKEN_KEY, token));

        /// <summary>
        /// Получить токен
        /// </summary>
        public static async Task<IResultValue<string>> GetToken() =>
            await BlobCache.Secure.GetAllKeys().
            ToTask().
            ToResultValueWhereTaskAsync(keys => keys.Contains(TOKEN_KEY),
                                    _ => TokenError).
            ResultValueOkBindAsync(_ => BlobCache.Secure.GetObject<string>(TOKEN_KEY).ToTask()).
            ResultValueBindOkTaskAsync(token => token.ToResultValueNullCheck(TokenError)).
            ResultValueBindOkTaskAsync(token => token.ToResultValueWhere(_ => !String.IsNullOrWhiteSpace(token),
                                                                         _ => TokenError));

        /// <summary>
        /// Очистить токен
        /// </summary>
        public static async Task ClearToken() =>
             await BlobCache.Secure.Invalidate(TOKEN_KEY);

        /// <summary>
        /// Ошибка получения токена
        /// </summary>
        private static IErrorResult TokenError =>
            new ErrorResult(ErrorResultType.ValueNotValid, "Токен не задан");
    }
}