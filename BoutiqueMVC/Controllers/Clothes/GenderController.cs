using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
using BoutiqueDTO.Models.Implementation.Clothes;
using BoutiqueMVC.Extensions.Controllers;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Extensions.Controllers.Sync;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
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
        public async Task<IActionResult> Get() =>
            await _genderService.GetGenders().
            ResultCollectionOkTaskAsync(GenderDtoConverter.ToDtoCollection).
            ToGetJsonResultCollectionTaskAsync();

        /// <summary>
        /// Записать типы полов для одежды
        /// </summary>
      //  [HttpPost]
        //public async Task<IActionResult> Post([FromBody] IList<GenderDto> gendersDto) =>
        //    await GenderDtoConverter.FromDtoCollection(gendersDto).
        //    MapAsync(genders => _genderService.UploadGenders(genders)).
        //    MapTaskAsync(gendersResult => gendersResult.ToGetActionResult());
    }
}
