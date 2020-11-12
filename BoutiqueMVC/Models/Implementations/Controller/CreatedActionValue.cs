using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Models.Implementations.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте cо значением
    /// </summary>
    public class CreatedActionValue<TValue> : CreatedActionBase
        where TValue : notnull
    {
        public CreatedActionValue(string actionGetName, string controllerName, TValue value)
            : base(actionGetName, controllerName)
        {
            Value = value;
        }

        /// <summary>
        /// Записанные значения
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Преобразовать в ответ контроллера о создании объекта
        /// </summary>
        public CreatedAtActionResult ToCreatedAtActionResult<TId>(TId idResult) =>
            new CreatedAtActionResult(ActionGetName, ControllerName, new { id = idResult }, Value);
    }
}