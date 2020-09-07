using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Infrastructure.Implementations.Converters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueMVC.Controllers.Interfaces.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Models.Implementations.Controller;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BoutiqueMVC.Controllers.Implementations.Base
{
    /// <summary>
    /// Базовый контроллер для Api
    /// </summary>
    public abstract class ApiController<TId, TTransfer, TDomain> : ControllerBase, IApiController<TId, TTransfer>
        where TTransfer : ITransferModel<TId>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        protected ApiController(IDatabaseService<TId, TDomain> databaseService,
                                ITransferConverter<TId, TDomain , TTransfer> transferConverter)
        {
            _databaseService = databaseService;
            _transferConverter = transferConverter;
        }

        /// <summary>
        /// Сервис получения данных из базы
        /// </summary>
        private readonly IDatabaseService<TId, TDomain> _databaseService;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly ITransferConverter<TId, TDomain, TTransfer> _transferConverter;

        /// <summary>
        /// Базовый метод получения данных
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get() =>
            await _databaseService.Get().
            ResultCollectionOkTaskAsync(_transferConverter.ToTransfers).
            ToGetJsonResultCollectionTaskAsync();

        /// <summary>
        /// Базовый метод отправки данных
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult<TTransfer>> Get(TId id) =>
            await _databaseService.Get(id).
            ResultValueOkTaskAsync(_transferConverter.ToTransfer).
            ToGetJsonResultTaskAsync();

        /// <summary>
        /// Записать данные
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(IList<TTransfer> transfers) =>
            await _transferConverter.FromTransfers(transfers).ToList().
            MapAsync(domains => _databaseService.Post(domains).
                                ToPostActionResultTaskAsync(GetCreateAction(domains)));

        /// <summary>
        /// Заменить данные по идентификатору
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> Put(TId id, TTransfer transfer);

        /// <summary>
        /// Удалить данные по идентификатору
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public abstract Task<IActionResult<TTransfer>> Delete(TId id);

        /// <summary>
        /// Получить информацию о создаваемом объекте на основе контроллера
        /// </summary>
        private CreatedActionCollection<TValue> GetCreateAction<TValue>(IEnumerable<TValue> values) =>
            new CreatedActionCollection<TValue>(nameof(Get), GetType().Name, values);
    }
}