using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Models.Implementations.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте c коллекцией
    /// </summary>
    public class CreatedActionCollection<TId, TValue> : CreatedActionBase
        where TId : notnull
        where TValue : notnull
    {
        public CreatedActionCollection(string actionGetName, string controllerName, 
                                       IEnumerable<(TId, TValue)> idValues)
            :base(actionGetName, controllerName)
        {
            IdValues = idValues.ToList().AsReadOnly();
        }

        /// <summary>
        /// Записанные значения
        /// </summary>
        public IReadOnlyCollection<(TId Id, TValue Value)> IdValues { get; }

        /// <summary>
        /// Идентификаторы
        /// </summary>
        public IEnumerable<TId> Ids => IdValues.Select(idValue => idValue.Id);

        /// <summary>
        /// Значения
        /// </summary>
        public IEnumerable<TValue> Values => IdValues.Select(idValue => idValue.Value);

        /// <summary>
        /// Преобразовать в ответ контроллера о создании объекта
        /// </summary>
        public CreatedAtActionResult ToCreatedAtActionResult() =>
            new CreatedAtActionResult(ActionGetName, ControllerName,
                 new { ids = IdValues.Select(idValue => idValue.Id) },
                 IdValues.Select(idValue => idValue.Value));
    }
}