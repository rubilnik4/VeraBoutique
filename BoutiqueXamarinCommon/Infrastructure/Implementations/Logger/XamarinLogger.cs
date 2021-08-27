using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Logger
{
    /// <summary>
    /// Логгер для клиента
    /// </summary>
    public class BoutiqueXamarinLogger: IBoutiqueLogger
    {
        /// <summary>
        /// Показать сообщение
        /// </summary>
        public void ShowMessage(string message)
        { }

        /// <summary>
        /// Показать ошибку
        /// </summary>
        public void ShowError(IResultError resultError)
        { }

        /// <summary>
        /// Показать ошибки
        /// </summary>
        public void ShowErrors(IEnumerable<IErrorResult> errors)
        { }
    }
}