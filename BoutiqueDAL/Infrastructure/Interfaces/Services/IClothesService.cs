﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// Сервис загрузки данных в базу для категорий одежды
    /// </summary>
    public interface IClothesService
    {
        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        Task<IResultCollection<Gender>> GetGenders();

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        Task <IResultError> UploadGenders(IEnumerable<Gender> genders);
    }
}