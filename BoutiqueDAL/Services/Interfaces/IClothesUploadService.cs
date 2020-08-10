using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Entities.Clothes;

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
        Task UploadSexTypes(IReadOnlyList<SexEntity> sexTypes);
    }
}