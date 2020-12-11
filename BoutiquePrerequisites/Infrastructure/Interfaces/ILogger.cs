using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Interfaces
{
    /// <summary>
    /// Отображение сообщений
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Показать сообщение
        /// </summary>
        string ShowMessage(string message);

        /// <summary>
        /// Показать ошибку
        /// </summary>
        string ShowError(IResultError resultError);
    }
}