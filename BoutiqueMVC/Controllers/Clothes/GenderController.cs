using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
using BoutiqueDTO.Models.Implementation.Clothes;
using BoutiqueMVC.Controllers.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : BaseApiController
    {
        /// <summary>
        /// Сервис загрузки данных в базу для категорий одежды
        /// </summary>
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        /// <summary>
        /// Получить типы полов для одежды
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override async Task<IActionResult> Get() =>
            await _genderService.GetGenders().
            ResultCollectionOkTaskAsync(GenderDtoConverter.ToDtoCollection).
            ToGetJsonResultCollectionTaskAsync();

        /// <summary>
        /// Получить тип пола одежды
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public override async Task<IActionResult> GetById<TId>(TId id) =>
            await Get();

        /// <summary>
        /// Записать типы полов для одежды
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] IList<GenderDto> gendersDto) =>
            await GenderDtoConverter.FromDtoCollection(gendersDto).ToList().
            MapAsync(genders => _genderService.UploadGenders(genders).
                                ToPostActionResultTaskAsync(GetCreateAction(genders)));
    }
}
