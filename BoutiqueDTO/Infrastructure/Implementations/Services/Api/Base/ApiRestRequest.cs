using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Sync;
using RestSharp;
using RestSharp.Serialization.Json;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base
{
    /// <summary>
    /// Запросы к серверу
    /// </summary>
    public static class ApiRestRequest
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
        /// Путь параметра идентификатора
        /// </summary>
        private static string Id => nameof(Id).ToLowerInvariant();

        /// <summary>
        /// Путь параметра коллекции
        /// </summary>
        private static string Collection => nameof(Collection).ToLowerInvariant();

        /// <summary>
        /// Запрос на получение данных
        /// </summary>
        public static IRestRequest GetJsonRequest(string controllerName) =>
           GetJsonRequest(controllerName, String.Empty);

        /// <summary>
        /// Запрос на получение данных
        /// </summary>
        public static IRestRequest GetJsonRequest(string controllerName, string additionalRoute) =>
            ValidateRoute(additionalRoute).
            Map(route => new RestRequest(GetApiRoute(controllerName) + route, Method.GET));

        /// <summary>
        /// Запрос на получение данных
        /// </summary>
        public static IRestRequest GetJsonRequest(string controllerName, IEnumerable<string> parameters) =>
            parameters.
            Aggregate(String.Empty, (first, second) => first + ValidateRoute(second)).
            Map(route => new RestRequest(GetApiRoute(controllerName) + route, Method.GET));

        /// <summary>
        /// Запрос на получение данных по идентификатору
        /// </summary>
        public static IRestRequest GetJsonRequest<TId>(TId id, string controllerName)
            where TId : notnull =>
            new RestRequest(GetApiRoute(controllerName) + "/{id}", Method.GET).
            AddUrlSegment(Id, id);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest PostJsonRequest<TId, TTransfer>(TTransfer transfer, string controllerName)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(GetApiRoute(controllerName), Method.POST).
            AddJsonBody(transfer);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest PostJsonRequest<TId, TTransfer>(IEnumerable<TTransfer> transfers, string controllerName)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(GetApiRoute(controllerName) + $"/{Collection}", Method.POST).
            AddJsonBody(transfers);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest PutJsonRequest<TId, TTransfer>(TTransfer transfer, string controllerName)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(GetApiRoute(controllerName), Method.PUT).
            AddJsonBody(transfer);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest DeleteJsonRequest(string controllerName)=>
            new RestRequest(GetApiRoute(controllerName), Method.DELETE);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest DeleteJsonRequest<TId>(TId id, string controllerName)
            where TId : notnull =>
            new RestRequest(GetApiRoute(controllerName) + $"/{Id}", Method.DELETE).
            AddUrlSegment(Id, id);

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