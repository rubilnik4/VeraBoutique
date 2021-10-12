using System;
using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueConsole.Infrastructure.Logger
{
    /// <summary>
    /// Отображение в консоли
    /// </summary>
    public class ConsoleBoutiqueLogger: IBoutiqueLogger
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
            foreach (var error in errors) Console.WriteLine($@"Ошибка [{error}]. {error.Description}");
        }
    }
}