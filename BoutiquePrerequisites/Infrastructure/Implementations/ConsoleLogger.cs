using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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
        public void ShowError(IResultError resultError) =>
            ShowErrors(resultError.Errors);

        /// <summary>
        /// Показать ошибки
        /// </summary>
        public void ShowErrors(IEnumerable<IErrorResult> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine($"Ошибка [{error.ErrorResultType}]. {error.Description}");
            }
        }
    }
}