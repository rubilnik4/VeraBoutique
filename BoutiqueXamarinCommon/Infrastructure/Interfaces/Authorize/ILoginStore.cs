using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Akavache;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize
{
    /// <summary>
    /// Хранение и загрузка данных аутентификации
    /// </summary>
    public interface ILoginStore
    {
        /// <summary>
        /// Сохранить токен
        /// </summary>
        Task<IResultError> SaveToken(string token);

        /// <summary>
        /// Получить токен
        /// </summary>
        Task<IResultValue<string>> GetToken();

        /// <summary>
        /// Очистить токен
        /// </summary>
        Task ClearToken();
    }
}