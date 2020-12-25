using System.Collections.Generic;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.Logger
{
    /// <summary>
    /// Отображение сообщений
    /// </summary>
    public interface ILogger
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