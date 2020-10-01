using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Interfaces.Base
{
    /// <summary>
    /// Базовый контроллер для Api
    /// </summary>
    public interface IApiController<TId, TTransfer>
        where TTransfer: ITransferModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Получить данные
        /// </summary>
        Task<ActionResult<IReadOnlyCollection<TTransfer>>> Get();

        ///// <summary>
        ///// Получить данные по идентификатору
        ///// </summary>
        Task<ActionResult<TTransfer>> Get(TId id);

        /// <summary>
        /// Записать данные
        /// </summary>
        Task<ActionResult<IReadOnlyCollection<TId>>> Post(IList<TTransfer> transfers);

        /// <summary>
        /// Заменить данные по идентификатору
        /// </summary>
        Task<IActionResult> Put(TTransfer transfer);

        /// <summary>
        /// Удалить данные по идентификатору
        /// </summary>
        Task<ActionResult<TTransfer>> Delete(TId id);
    }
}