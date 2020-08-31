using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
using BoutiqueMVC.Extensions.Controllers;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
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
            ResultCollectionOkToValueTaskAsync(GenderDtoConverter.ToJsonCollection).
            MapTaskAsync(gendersJson => gendersJson.ToActionResult());

        /// <summary>
        /// Записать типы полов для одежды
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenderType genderType)
        {
            var genders = new List<Gender>()
            {
                new Gender(GenderType.Female, "Женшина"),
                new Gender(GenderType.Male, "Мушина"),
            };
            await _genderService.UploadGenders(genders);
            return Ok("ok");
        }
    }
}
