﻿using System.Collections.Generic;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;
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
                                                     GetJsonError<TValue>(json));

        /// <summary>
        /// Преобразовать json в трансферные модели
        /// </summary>
        public static IResultCollection<TValue> ToTransferCollectionJson<TValue>(this string json)
           where TValue : notnull =>
            ResultCollectionTryExtensions.ResultCollectionTry(() => JsonConvert.DeserializeObject<List<TValue>>(json),
                                                              GetJsonListError<TValue>(json));

        /// <summary>
        /// Преобразовать json в трансферную модель
        /// </summary>
        public static IResultValue<string> ToJsonTransfer<TValue>(this TValue transfer)
            where TValue : notnull =>
            ResultValueTryExtensions.ResultValueTry(() => JsonConvert.SerializeObject(transfer),
                                                    GetJsonError(transfer));

        /// <summary>
        /// Преобразовать json в трансферные модели
        /// </summary>
        public static IResultValue<string> ToJsonTransfer<TValue>(this IEnumerable<TValue> transfers)
            where TValue : notnull =>
            ResultValueTryExtensions.ResultValueTry(() => JsonConvert.SerializeObject(transfers),
                                                    GetJsonListError(transfers));

        /// <summary>
        /// Ошибка конвертации из Json
        /// </summary>
        private static IErrorResult GetJsonError<TValue>(string json)
            where TValue : notnull =>
            ErrorResultFactory.DeserializeError<TValue>(ConvertionErrorType.JsonConvertion, json, $"Ошибка десериализации Json типа [{typeof(TValue).Name}]");

        /// <summary>
        /// Ошибка конвертации из Json коллекции
        /// </summary>
        private static IErrorResult GetJsonListError<TValue>(string json)
            where TValue : notnull =>
            ErrorResultFactory.DeserializeError<TValue>(ConvertionErrorType.JsonConvertion, json, $"Ошибка десериализации Json коллекции типа [{typeof(List<TValue>).Name}]");

        /// <summary>
        /// Ошибка конвертации в Json
        /// </summary>
        private static IErrorResult GetJsonError<TValue>(TValue value)
                where TValue : notnull =>
                ErrorResultFactory.SerializeError(ConvertionErrorType.JsonConvertion, value, $"Ошибка сериализации Json типа [{typeof(TValue).Name}]");

        /// <summary>
        /// Ошибка конвертации в Json коллекции
        /// </summary>
        private static IErrorResult GetJsonListError<TValue>(IEnumerable<TValue> values)
            where TValue : notnull =>
            ErrorResultFactory.SerializeError(ConvertionErrorType.JsonConvertion, values, $"Ошибка сериализации Json коллекцию типа [{typeof(List<TValue>).Name}]");
    }
}