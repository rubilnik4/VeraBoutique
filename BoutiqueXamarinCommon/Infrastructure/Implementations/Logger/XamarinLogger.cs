using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

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