using System;

namespace BoutiquePrerequisites.Models.Interfaces.Connection
{
    /// <summary>
    /// Параметры подключения
    /// </summary>
    public interface IHostConnection
    {
        /// <summary>
        /// Имя сервера
        /// </summary>
        Uri Host { get; }

        /// <summary>
        /// Время ожидания
        /// </summary>
        int TimeOut { get; }
    }
}