using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Newtonsoft.Json;

namespace BoutiqueDTO.Extensions.Json.Sync
{
    /// <summary>
    /// Методы расширения для json
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Преобразовать json в трансферную модель
        /// </summary>
        public static IResultValue<TTransfer> ToTransferJson<TId, TTransfer>(this string json)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            ResultValueTryExtensions.ResultValueTry(() => JsonConvert.DeserializeObject<TTransfer>(json),
                                                     GetJsonError<TId, TTransfer>());

        /// <summary>
        /// Ошибка конвертации в Json
        /// </summary>
        private static IErrorResult GetJsonError<TId, TTransfer>()
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new ErrorResult(ErrorResultType.JsonConvertion, $"Ошибка конвертации в Json типа [{typeof(TTransfer).Name}]");
    }
}