using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Enums;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Services.Interfaces;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Services.Implementations
{
    /// <summary>
    /// Сервис загрузки данных в базу для категорий одежды
    /// </summary>
    public class ClothesUploadService : IClothesUploadService
    {
        /// <summary>
        /// Получить обертку управления транзакциями
        /// </summary>
        private readonly Func<IUnitOfWork> _getUnitOfWork;

        public ClothesUploadService(Func<IUnitOfWork> getUnitOfWork)
        {
            _getUnitOfWork = getUnitOfWork;
        }

        /// <summary>
        /// Загрузить пол в базу данных
        /// </summary>
        public async Task<IResultError> UploadSexTypes(IReadOnlyList<SexEntity> sexTypes) =>
            await _getUnitOfWork.Invoke().
            UseAndCommitAsync(session => sexTypes.Select(sexType => session.SaveOrUpdateAsync(sexType)).
                                                  WaitAll());
    }
}