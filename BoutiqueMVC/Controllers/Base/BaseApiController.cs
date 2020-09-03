using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Implementation.Clothes;
using BoutiqueMVC.Controllers.Clothes;
using BoutiqueMVC.Models.Implementations.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Base
{
    /// <summary>
    /// Базовый контроллер для Api
    /// </summary>
    public abstract class BaseApiController: ControllerBase
    {
        /// <summary>
        /// Базовый метод получения данных
        /// </summary>
        public abstract Task<IActionResult> Get();

        /// <summary>
        /// Базовый метод отправки данных
        /// </summary>
        public abstract Task<IActionResult> GetById<TId>(TId id)
            where TId : IEquatable<TId>;

        /// <summary>
        /// Получить информацию о создаваемом объекте на основе контроллера
        /// </summary>
        protected CreatedActionCollection<TValue> GetCreateAction<TValue>(IEnumerable<TValue> values) =>
            new CreatedActionCollection<TValue>(nameof(GetById), GetType().Name, values);
    }
}