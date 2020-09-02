using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Implementation.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// Сервис загрузки данных в базу для типа пола одежды
    /// </summary>
    public interface IGenderService
    {
        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        Task<IResultCollection<Gender>> GetGenders();

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        Task<IResultCollection<int>> UploadGenders(IEnumerable<Gender> genders);
    }
}