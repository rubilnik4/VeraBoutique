using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BoutiqueMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        /// <summary>
        /// Сервис загрузки данных в базу для категорий одежды
        /// </summary>
        private readonly IClothesUploadService _clothesUploadService;

        public ClothesController(IClothesUploadService clothesUploadService)
        {
            _clothesUploadService = clothesUploadService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var collectionSex = new List<SexEntity>()
            {
                new SexEntity(){Name = "Мужчина", Type = SexType.Male}
            };
            await _clothesUploadService.UploadSexTypes(collectionSex);

            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
