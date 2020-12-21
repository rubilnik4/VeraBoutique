using System;
using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Models.Implementations.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте cо значением
    /// </summary>
    public class CreatedActionValue<TId, TValue> : CreatedActionBase
        where TId : notnull
        where TValue : notnull
    {
        public CreatedActionValue(string actionGetName, string controllerName,
                                  (TId, TValue) idValue)
            : base(actionGetName, controllerName)
        {
            IdValue = idValue;
        }

        /// <summary>
        /// Записанные значения с идентификатором
        /// </summary>
        public (TId Id, TValue Value) IdValue { get; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public TId Id => IdValue.Id;

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value => IdValue.Value;

        /// <summary>
        /// Преобразовать в ответ контроллера о создании объекта
        /// </summary>
        public CreatedAtActionResult ToCreatedAtActionResult() =>
            new (ActionGetName, ControllerName, new { id = IdValue.Id }, IdValue.Value);
    }
}