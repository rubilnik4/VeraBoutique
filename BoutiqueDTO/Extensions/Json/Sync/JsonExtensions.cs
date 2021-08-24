using System.Collections.Generic;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Enums;
using Functional.Models.Implementations.Results;
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
        public static IResultValue<TValue> ToTransferValueJson<TValue>(this string json)
            where TValue : notnull =>
            ResultValueTryExtensions.ResultValueTry(() => JsonConvert.DeserializeObject<TValue>(json),
                                                     GetJsonError<TValue>());

        /// <summary>
        /// Преобразовать json в трансферные модели
        /// </summary>
        public static IResultCollection<TValue> ToTransferCollectionJson<TValue>(this string json)
           where TValue : notnull =>
            ResultCollectionTryExtensions.ResultCollectionTry(() => JsonConvert.DeserializeObject<List<TValue>>(json),
                                                              GetJsonListError<TValue>());

        /// <summary>
        /// Преобразовать json в трансферную модель
        /// </summary>
        public static IResultValue<string> ToJsonTransfer<TValue>(this TValue transfer)
            where TValue : notnull =>
            ResultValueTryExtensions.ResultValueTry(() => JsonConvert.SerializeObject(transfer),
                                                    GetJsonError<TValue>());

        /// <summary>
        /// Преобразовать json в трансферные модели
        /// </summary>
        public static IResultValue<string> ToJsonTransfer<TValue>(this IEnumerable<TValue> transfers)
            where TValue : notnull =>
            ResultValueTryExtensions.ResultValueTry(() => JsonConvert.SerializeObject(transfers),
                                                    GetJsonError<TValue>());

        /// <summary>
        /// Ошибка конвертации в Json
        /// </summary>
        private static IErrorResult GetJsonError<TValue>()
            where TValue : notnull =>
            ConvertionErrorType.JsonConvertion.ToErrorTypeResult($"Ошибка конвертации Json типа [{typeof(TValue).Name}]");

        /// <summary>
        /// Ошибка конвертации в Json коллекции
        /// </summary>
        private static IErrorResult GetJsonListError<TValue>()
            where TValue : notnull =>
            ConvertionErrorType.JsonConvertion.ToErrorTypeResult($"Ошибка конвертации Json коллекцию типа [{typeof(List<TValue>).Name}]");
    }
}