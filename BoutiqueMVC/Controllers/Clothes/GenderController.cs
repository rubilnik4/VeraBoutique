using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
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
        private readonly IClothesService _clothesService;

        public GenderController(IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        /// <summary>
        /// Получить типы полов для одежды
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _clothesService.GetGenders());
      

        ///// <summary>
        ///// Записать типы полов для одежды
        ///// </summary>
        //[HttpPost]
        //public Task Post([FromBody] GenderType genderType)
        //{

        //}
    }
}
