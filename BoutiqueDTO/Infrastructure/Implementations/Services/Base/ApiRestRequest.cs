using System.Collections.Generic;
using BoutiqueDTO.Models.Interfaces.Base;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Запросы к серверу
    /// </summary>
    public static class ApiRestRequest
    {
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
        public static IRestRequest GetJsonRequest<TId, TTransfer>()
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(ApiRoutes.GetApiRoute<TId, TTransfer>(), Method.GET, DataFormat.Json);

        /// <summary>
        /// Запрос на получение данных по идентификатору
        /// </summary>
        public static IRestRequest GetJsonRequest<TId, TTransfer>(TId id)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(ApiRoutes.GetApiRoute<TId, TTransfer>() + $"/{Id}", Method.GET, DataFormat.Json).
            AddUrlSegment(Id, id);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest PostJsonRequest<TId, TTransfer>(TTransfer transfer)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(ApiRoutes.GetApiRoute<TId, TTransfer>(), Method.POST, DataFormat.Json).
            AddJsonBody(transfer);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest PostJsonRequest<TId, TTransfer>(IEnumerable<TTransfer> transfers)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(ApiRoutes.GetApiRoute<TId, TTransfer>() + $"/{Collection}", Method.POST, DataFormat.Json).
            AddJsonBody(transfers);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest PutJsonRequest<TId, TTransfer>(TTransfer transfer)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(ApiRoutes.GetApiRoute<TId, TTransfer>(), Method.PUT, DataFormat.Json).
            AddJsonBody(transfer);

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        public static IRestRequest DeleteJsonRequest<TId, TTransfer>(TId id)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            new RestRequest(ApiRoutes.GetApiRoute<TId, TTransfer>(), Method.DELETE, DataFormat.Json).
            AddUrlSegment(Id, id);
    }
}