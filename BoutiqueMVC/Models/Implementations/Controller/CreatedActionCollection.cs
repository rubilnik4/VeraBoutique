using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Models.Implementations.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте c коллекцией
    /// </summary>
    public class CreatedActionCollection<TValue>: CreatedActionBase
        where TValue: notnull
    {
        public CreatedActionCollection(string actionGetName, string controllerName, IEnumerable<TValue> values)
            :base(actionGetName, controllerName)
        {
            Values = values.ToList().AsReadOnly();
        }

        /// <summary>
        /// Записанные значения
        /// </summary>
        public IReadOnlyCollection<TValue> Values { get; }

        /// <summary>
        /// Преобразовать в ответ контроллера о создании объекта
        /// </summary>
        public CreatedAtActionResult ToCreatedAtActionResult<TId>(IEnumerable<TId> idResults) =>
            new CreatedAtActionResult(ActionGetName, ControllerName, new { ids = idResults }, Values);
    }
}