using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiquePrerequisites.Factories.Client;
using BoutiquePrerequisites.Factories.Connection;
using BoutiquePrerequisites.Infrastructure.Implementations;
using BoutiquePrerequisites.Infrastructure.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;

namespace BoutiquePrerequisites
{
    public class Program
    {
        /// <summary>
        /// Стартовый метод
        /// </summary>
        public static async Task Main() =>
           await BoutiqueClientInitialize(new ConsoleLogger());

        /// <summary>
        /// Инициализация клиента
        /// </summary>
        private static async Task BoutiqueClientInitialize(ILogger logger) =>
            new ResultError().
            Void(_ => logger.ShowMessage("Инициализация клиента отправки")).
            ToResultValueBind(BoutiqueClientFactory.BoutiqueClient).
            Void(_ => logger.ShowMessage("Завершено"));
    }
}
