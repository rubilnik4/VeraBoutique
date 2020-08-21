using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using Functional.FunctionalExtensions.Async;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Get()

        {
            var genders = new List<Gender>()
            {
                new Gender(GenderType.Female, "Женшина"),
                new Gender(GenderType.Female, "Мушина"),
            };
            await _genderService.UploadGenders(genders);
            return Ok("ok");
        }
        //await _genderService.GetGenders().
        //MapTaskAsync(genders => Ok(genders));


        ///// <summary>
        ///// Записать типы полов для одежды
        ///// </summary>
        //[HttpPost]
        //public Task Post([FromBody] GenderType genderType)
        //{

        //}
    }
}
