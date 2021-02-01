using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Newtonsoft.Json;

namespace BoutiqueDTO.Extensions.Json.Async
{
    /// <summary>
    /// Асинхронные методы расширения для json
    /// </summary>
    public static class JsonAsyncExtensions
    {
        /// <summary>
        /// Преобразовать json в трансферную модель
        /// </summary>
        public static async Task<IResultValue<TTransfer>> ToTransferJsonAsync<TId, TTransfer>(this Task<string> json)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            await json.MapTaskAsync(jsonAwaited => jsonAwaited.ToTransferJson<TId, TTransfer>());
    }
}