using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueDTO.Models.Interfaces.Connection;
using BoutiquePrerequisites.Factories.Connection;
using BoutiquePrerequisites.Infrastructure.Implementations;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites
{
    public class Program
    {
        /// <summary>
        /// Стартовый метод
        /// </summary>
        public static async Task Main() =>
           await BoutiqueConnection.BoutiqueHostConnection.
           ResultValueBindErrorsOkAsync(hostConnection => BoutiqueClientInitialize(hostConnection, new ConsoleLogger()));

        /// <summary>
        /// Инициализация клиента
        /// </summary>
        private static async Task<IResultError> BoutiqueClientInitialize(IHostConnection hostConnection, ILogger logger) =>
            await new ResultError().
            Void(_ => logger.ShowMessage("Инициализация клиента отправки")).
            ResultErrorBindOkAsync(() => GenderUpload.Upload(hostConnection, logger)).
            VoidTaskAsync(_ => logger.ShowMessage("Завершено"));
    }
}
