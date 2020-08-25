using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.FunctionalExtensions.Sync;
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
        public async Task<IReadOnlyCollection<Gender>> GetGenders() =>
            _boutiqueDatabase.
            ResultValueOkAsync(boutiqueDatabase => boutiqueDatabase.Genders.ToListAsync()).
            Map(tt => tt)
            ResultValueOkAsync(genders => genders.Select(GenderEntityConverter.FromEntity).ToList().AsReadOnly());

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task UploadGenders(IEnumerable<Gender> genders) =>
            await genders.Select(GenderEntityConverter.ToEntity).
            VoidAsync(genderEntities => _boutiqueDatabase.Genders.AddRangeAsync(genderEntities)).
            VoidBindAsync(_ => _boutiqueDatabase.SaveChangesAsync());
    }
}