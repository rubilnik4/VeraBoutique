using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Models.Implementations.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте
    /// </summary>
    public class CreatedActionCollection<TValue>
    {
        public CreatedActionCollection(string actionGetName, string controllerName, IEnumerable<TValue> values)
        {
            ActionGetName = actionGetName;
            ControllerName = controllerName;
            Values = values.ToList().AsReadOnly();
        }

        /// <summary>
        /// Наименование пути для получения данных
        /// </summary>
        public string ActionGetName { get; }

        /// <summary>
        /// Наименование контроллера
        /// </summary>
        public string ControllerName { get; }

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