using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Entities.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Services.Interfaces
{
    /// <summary>
    /// Сервис загрузки данных в базу для категорий одежды
    /// </summary>
    public interface IClothesUploadService
    {
        /// <summary>
        /// Загрузить пол в базу данных
        /// </summary>
        Task<IResultError> UploadSexTypes(IReadOnlyList<SexEntity> sexTypes);
    }
}