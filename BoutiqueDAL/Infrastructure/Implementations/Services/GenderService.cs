using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Сервис загрузки данных в базу для типа пола одежды
    /// </summary>
    public class GenderService : IGenderService
    {
        /// <summary>
        /// Получить обертку управления транзакциями
        /// </summary>
        private readonly BoutiqueDatabase _boutiqueDatabase;

        public GenderService()
        {
            _boutiqueDatabase = new BoutiqueDatabase();
        }

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task<IResultCollection<Gender>> GetGenders() =>
            await _boutiqueDatabase.Genders.ToListAsync().
            MapTaskAsync(genders => genders.Select(GenderEntityConverter.FromEntity)).
            ToResultCollectionTaskAsync();
            //await _getUnitOfWork.Invoke().
            //UseFuncAsync(session => session.Query<GenderEntity>().ToListAsync().
            //                        MapTaskAsync(genders => genders.Select(GenderEntityConverter.FromEntity))).
            //ToResultCollectionTaskAsync();

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task<IResultError> UploadGenders(IEnumerable<Gender> genders) =>
            await _getUnitOfWork.Invoke().
            UseActionAndCommitAsync(session => genders.
                                               Select(GenderEntityConverter.ToEntity).
                                               Select(gender => session.SaveOrUpdateAsync(gender)).
                                               WaitAll());
    }
}