using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Сервис загрузки данных в базу для типа пола одежды
    /// </summary>
    public class GenderService : IGenderService
    {
        /// <summary>
        /// База данных магазина
        /// </summary>
        private readonly IResultValue<IBoutiqueDatabase> _boutiqueDatabase;

        public GenderService(IResultValue<IBoutiqueDatabase> boutiqueDatabase)
        {
            _boutiqueDatabase = boutiqueDatabase;
        }

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task<IResultCollection<Gender>> GetGenders() =>
            await _boutiqueDatabase.
            ResultValueBindOkAsync(boutiqueDatabase => boutiqueDatabase.GendersTable.ToListAsync().ToResultValue()).
            ResultValueOkTaskAsync(genders => genders.Select(GenderEntityConverter.FromEntity).ToList().AsReadOnly()).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task<IResultCollection<GenderType>> UploadGenders(IEnumerable<Gender> genders) =>
            await _boutiqueDatabase.
            ResultValueBindOkToCollectionAsync(boutiqueDatabase => boutiqueDatabase.GendersTable.
                                                               AddRangeAsync(genders.Select(GenderEntityConverter.ToEntity)).
                                                               ResultCollectionVoidOkBindAsync(_ => boutiqueDatabase.SaveChangesAsync()));
    }
}