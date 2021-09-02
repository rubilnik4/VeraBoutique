using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueCommon.Infrastructure.Interfaces.Logger
{
    /// <summary>
    /// Отображение сообщений
    /// </summary>
    public interface IBoutiqueLogger
    {
        /// <summary>
        /// Показать сообщение
        /// </summary>
        void ShowMessage(string message);

        /// <summary>
        /// Показать ошибку
        /// </summary>
        void ShowError(IResultError resultError);

        /// <summary>
        /// Показать ошибки
        /// </summary>
        void ShowErrors(IEnumerable<IErrorResult> errors);
    }
}