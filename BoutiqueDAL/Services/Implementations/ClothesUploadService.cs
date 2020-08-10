using System.Collections.Generic;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using BoutiqueCommon.Models.Enums;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Services.Interfaces;

namespace BoutiqueDAL.Services.Implementations
{
    /// <summary>
    /// Сервис загрузки данных в базу для категорий одежды
    /// </summary>
    public class ClothesUploadService: IClothesUploadService
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
        public async Task UploadSexTypes(IReadOnlyList<SexEntity> sexTypes)
        {
            using var unitOfWork = _getUnitOfWork();

            foreach (var sexType in sexTypes)
            {
                await unitOfWork.Session.SaveOrUpdateAsync(sexType);
            }

            await unitOfWork.CommitAsync();
        }
    }
}