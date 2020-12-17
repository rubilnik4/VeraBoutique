using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Sync;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Создание пути для обращения к сервису
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// Начальный путь для
        /// </summary>
        private const string API = "Api/";

        /// <summary>
        /// Путь обращения к контроллеру
        /// </summary>
        public static string GetApiRoute<TId, TTransfer>()
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            API + GetControllerRoute<TId, TTransfer>();

        /// <summary>
        /// Получить путь к контроллеру
        /// </summary>
        private static string GetControllerRoute<TId, TTransfer>()
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            typeof(TTransfer).Name.
            Map(transferType => transferType.Remove(transferType.Length - "Transfer".Length));
    }
}