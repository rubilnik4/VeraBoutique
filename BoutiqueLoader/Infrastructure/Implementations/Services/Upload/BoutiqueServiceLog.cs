using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueLoader.Models.Enums;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
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
            result.
            ResultErrorVoidOkBad(() => boutiqueLogger.ShowMessage($"{serviceActionType} [{typeof(TDomain).Name}] completed"),
                                 errors => errors.
                                           Void(_ => boutiqueLogger.ShowMessage($"Error {serviceActionType} [{typeof(TDomain).Name}]")).
                                           Void(_ => boutiqueLogger.ShowErrors(errors)));
    }
}