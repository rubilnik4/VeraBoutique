using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueMVC.Controllers.Interfaces.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Models.Enums.Identity;
using BoutiqueMVC.Models.Implementations.Controller;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BoutiqueMVC.Models.Enums.Identity.IdentityPolicyType;

namespace BoutiqueMVC.Controllers.Implementations.Base
{
    /// <summary>
    /// Базовый контроллер для Api
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ADMIN_POLICY)]
    [ApiController]
    public abstract class ApiController<TId, TDomain, TTransfer> : 
        ControllerBase, IApiController<TId, TTransfer>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        protected ApiController(IDatabaseService<TId, TDomain> databaseDatabaseService,
                                ITransferConverter<TId, TDomain, TTransfer> transferConverter)
        {
            _databaseDatabaseService = databaseDatabaseService;
            _transferConverter = transferConverter;
        }

        /// <summary>
        /// Сервис получения данных из базы
        /// </summary>
        private readonly IDatabaseService<TId, TDomain> _databaseDatabaseService;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly ITransferConverter<TId, TDomain, TTransfer> _transferConverter;

        /// <summary>
        /// Базовый метод получения данных
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<TTransfer>>> Get() =>
            await _databaseDatabaseService.Get().
            ResultCollectionOkTaskAsync(_transferConverter.ToTransfers).
            ToActionResultCollectionTaskAsync<TId, TTransfer>();

        /// <summary>
        /// Базовый метод получения данных по идентификатору
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TTransfer>> Get(TId id) =>
            await _databaseDatabaseService.Get(id).
            ResultValueOkTaskAsync(_transferConverter.ToTransfer).
            ToActionResultValueTaskAsync<TId, TTransfer>();

        /// <summary>
        /// Записать данные
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TId>> Post(TTransfer transfer) =>
            await _transferConverter.FromTransfer(transfer).
            ResultValueBindOkAsync(domain => _databaseDatabaseService.Post(domain)).
            ToActionResultValueTaskAsync();

        /// <summary>
        /// Записать данные
        /// </summary>
        [HttpPost("collection")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<TId>>> Post(IList<TTransfer> transfers) =>
            await _transferConverter.FromTransfers(transfers).
            ResultCollectionBindOkAsync(domains => _databaseDatabaseService.Post(domains)).
            ToActionResultCollectionTaskAsync();

        /// <summary>
        /// Заменить данные по идентификатору
        /// </summary>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(TTransfer transfer) =>
             await _transferConverter.FromTransfer(transfer).
             ResultValueBindErrorsOkAsync(domain => _databaseDatabaseService.Put(domain)).
             ToResultErrorTaskAsync().
             ToNoContentActionResultTaskAsync();

        /// <summary>
        /// Удалить все данные
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete() =>
            await _databaseDatabaseService.Delete().
            ToNoContentActionResultTaskAsync();

        /// <summary>
        /// Удалить данные по идентификатору
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TTransfer>> Delete(TId id) =>
            await _databaseDatabaseService.Delete(id).
            ResultValueOkTaskAsync(_transferConverter.ToTransfer).
            ToActionResultValueTaskAsync<TId, TTransfer>();
    }
}