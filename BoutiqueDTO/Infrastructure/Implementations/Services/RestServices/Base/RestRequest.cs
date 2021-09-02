using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Routes.Clothes;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base
{
    /// <summary>
    /// Запросы к серверу
    /// </summary>
    public static class RestRequest
    {
        /// <summary>
        /// Начальный путь для
        /// </summary>
        private const string API = "api/";

        /// <summary>
        /// Политика именования контроллера
        /// </summary>
        private const string CONTROLLER = "Controller";

        /// <summary>
        /// Запрос на получение данных
        /// </summary>
        public static string GetRequest(string controllerName) =>
           GetRequest(controllerName, String.Empty);

        /// <summary>
        /// Запрос на получение данных
        /// </summary>
        public static string GetRequest(string controllerName, string additionalRoute) =>
            ValidateRoute(additionalRoute).
            Map(route => GetApiRoute(controllerName + route));

        /// <summary>
        /// Запрос на получение данных
        /// </summary>
        public static string GetRequest(string controllerName, IEnumerable<string> parameters) =>
            parameters.
            Aggregate(String.Empty, (first, second) => first + ValidateRoute(second)).
            Map(route => GetApiRoute(controllerName) + route);

        /// <summary>
        /// Запрос на получение данных
        /// </summary>
        public static string GetRequest(string controllerName, string additionalRoute, IEnumerable<string> parameters) =>
            parameters.
            Aggregate(String.Empty, (first, second) => first + ValidateRoute(second)).
            Map(route => GetApiRoute(controllerName) + ValidateRoute(additionalRoute) + route);

        /// <summary>
        /// Запрос на получение данных по идентификатору
        /// </summary>
        public static string GetRequest<TId>(TId id, string controllerName, string additionalRoute)
            where TId : notnull =>
            GetRequest(id, controllerName + ValidateRoute(additionalRoute));

        /// <summary>
        /// Запрос на получение данных по идентификатору
        /// </summary>
        public static string GetRequest<TId>(TId id, string controllerName)
            where TId : notnull =>
            $"{GetApiRoute(controllerName)}/{id}";

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static string PostRequest(string controllerName) =>
           GetRequest(controllerName);

        /// <summary>
        /// Запрос на отправку данных коллекции
        /// </summary>
        public static string PostRequestCollection(string controllerName) =>
           GetRequest(controllerName, BaseRoutes.POST_COLLECTION_ROUTE);

        /// <summary>
        /// Путь обращения к контроллеру
        /// </summary>
        private static string GetApiRoute(string controllerName) =>
            API + controllerName.SubstringRemove(CONTROLLER);

        /// <summary>
        /// Проверка пути
        /// </summary>
        private static string ValidateRoute(string additionalRoute) =>
            additionalRoute.
            WhereContinue(route => !String.IsNullOrWhiteSpace(route),
                route => $"/{route}",
                _ => String.Empty);
    }
}