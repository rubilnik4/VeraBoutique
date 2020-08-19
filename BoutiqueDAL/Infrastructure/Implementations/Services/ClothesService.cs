using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using NHibernate.Linq;

namespace BoutiqueDAL.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Сервис загрузки данных в базу для категорий одежды
    /// </summary>
    public class ClothesService : IClothesService
    {
        /// <summary>
        /// Получить обертку управления транзакциями
        /// </summary>
        private readonly Func<IUnitOfWork> _getUnitOfWork;

        public ClothesService(Func<IUnitOfWork> getUnitOfWork)
        {
            _getUnitOfWork = getUnitOfWork;
        }

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task<IResultValue<List<Gender>>> GetGenders() =>
            await _getUnitOfWork.Invoke().
            Use(session => session.Query<GenderEntity>().ToListAsync().
                                   MapBindAsync(genders => genders.Select(GenderEntityConverter.FromEntity).ToList()));

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task<IResultError> UploadGenders(IReadOnlyList<GenderEntity> genders) =>
            await _getUnitOfWork.Invoke().
            UseAndCommitAsync(session => genders.Select(gender => session.SaveOrUpdateAsync(gender)).
                                                  WaitAll()).
            Map(tt =>  tt);
    }
}