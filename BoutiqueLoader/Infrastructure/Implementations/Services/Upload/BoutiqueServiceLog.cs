using System;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueLoader.Models.Enums;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.Models.Interfaces.Result;

namespace BoutiqueLoader.Infrastructure.Implementations.Services.Upload
{
    /// <summary>
    /// Логгирование обращений к сервису
    /// </summary>
    public static class BoutiqueServiceLog
    {
        /// <summary>
        /// Логгирование действия с базой
        /// </summary>
        public static void LogServiceAction<TId, TDomain>(IResultError result, IBoutiqueLogger boutiqueLogger,
                                                          ServiceActionType serviceActionType)
               where TDomain : IDomainModel<TId>
               where TId : notnull =>
            LogServiceAction<TId, TDomain>(result, boutiqueLogger, serviceActionType, String.Empty);

        /// <summary>
        /// Логгирование действия с базой
        /// </summary>
        public static void LogServiceAction<TId, TDomain>(IResultError result, IBoutiqueLogger boutiqueLogger,
                                                          ServiceActionType serviceActionType, int index, int indexMax)
               where TDomain : IDomainModel<TId>
               where TId : notnull =>
            LogServiceAction<TId, TDomain>(result, boutiqueLogger, serviceActionType, GetIndexMessage(index, indexMax));

        /// <summary>
        /// Логгирование действия с базой
        /// </summary>
        private static void LogServiceAction<TId, TDomain>(IResultError result, IBoutiqueLogger boutiqueLogger,
                                                          ServiceActionType serviceActionType, string indexMessage)
               where TDomain : IDomainModel<TId>
               where TId : notnull =>
            result.
            ResultErrorVoidOkBad(
                () => boutiqueLogger.ShowMessage($"{indexMessage}{serviceActionType} [{typeof(TDomain).Name}] completed"),
                errors => errors.
                          Void(_ => boutiqueLogger.ShowMessage($"{indexMessage}Error {serviceActionType} [{typeof(TDomain).Name}]")).
                          Void(_ => boutiqueLogger.ShowErrors(errors)));

        /// <summary>
        /// Сообщение о пакетах
        /// </summary>
        private static string GetIndexMessage(int index, int indexMax) =>
            $"[{index + 1} from {indexMax + 1}] ";
    }
}