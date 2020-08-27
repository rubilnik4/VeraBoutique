using System.Collections.Generic;

namespace Functional.Models.Interfaces.Result
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией
    /// </summary>
    public interface IResultCollection<out TValue> : IResultValue<IReadOnlyCollection<TValue>>
    {
        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultCollection<TValue> ConcatErrors(IEnumerable<IErrorResult> errors);

        /// <summary>
        /// Преобразовать в результирующий ответ со значением
        /// </summary>
        IResultValue<IReadOnlyCollection<TValue>> ToResultValue() => this;
    }
}