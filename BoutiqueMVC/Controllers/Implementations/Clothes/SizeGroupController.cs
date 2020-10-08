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
        [HttpGet("sizeGroupsInclude/{clothesSizeType}/{sizeNormalize}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<SizeGroupTransfer>>> GetSizeGroupsIncludeSize(ClothesSizeType clothesSizeType,
                                                                                                         int sizeNormalize) =>
            await _sizeGroupDatabaseService.GetSizeGroupsIncludeSize(clothesSizeType, sizeNormalize).
            ResultCollectionOkTaskAsync(sizeGroups => _sizeGroupTransferConverter.ToTransfers(sizeGroups)).
            ToActionResultCollectionTaskAsync<(ClothesSizeType, int), SizeGroupTransfer>();
    }
}