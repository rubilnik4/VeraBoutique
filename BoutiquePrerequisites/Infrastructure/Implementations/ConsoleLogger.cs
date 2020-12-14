using System;
using BoutiquePrerequisites.Infrastructure.Interfaces;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Implementations
{
    /// <summary>
    /// Отображение в консоли
    /// </summary>
    public class ConsoleLogger: ILogger
    {
        /// <summary>
        /// Показать сообщение
        /// </summary>
        public void ShowMessage(string message) =>
            Console.WriteLine(message);

        /// <summary>
        /// Показать ошибку
        /// </summary>
        public void ShowError(IResultError resultError)
        {
            foreach (var error in resultError.Errors)
            {
                Console.WriteLine($"Ошибка [{error.ErrorResultType}]. {error.Description}");
            }
        }
    }
}