using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи группы размеров одежды
    /// </summary>
    public class SizeGroupController : ApiController<(ClothesSizeType, int), SizeGroupTransfer, ISizeGroupDomain>
    {
        public SizeGroupController(ISizeGroupDatabaseService sizeGroupDatabaseService,
                                   ISizeGroupTransferConverter sizeGroupTransferConverter)
            : base(sizeGroupDatabaseService, sizeGroupTransferConverter)
        {
            _sizeGroupDatabaseService = sizeGroupDatabaseService;
            _sizeGroupTransferConverter = sizeGroupTransferConverter;
        }

        /// <summary>
        /// Сервис группы размеров одежды в базе данных
        /// </summary>
        private readonly ISizeGroupDatabaseService _sizeGroupDatabaseService;

        /// <summary>
        /// Конвертер группы размеров одежды в трансферную модель
        /// </summary>
        private readonly ISizeGroupTransferConverter _sizeGroupTransferConverter;

        /// <summary>
        /// Получить группу размеров одежды совместно с размерами
        /// </summary>
        [HttpGet("include/{clothesSizeType}/{sizeNormalize}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SizeGroupTransfer>> GetSizeGroupIncludeSize(ClothesSizeType clothesSizeType, int sizeNormalize) =>
            await _sizeGroupDatabaseService.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize).
            ResultValueOkTaskAsync(sizeGroup => _sizeGroupTransferConverter.ToTransfer(sizeGroup)).
            ToActionResultValueTaskAsync<(ClothesSizeType, int), SizeGroupTransfer>();
    }
}