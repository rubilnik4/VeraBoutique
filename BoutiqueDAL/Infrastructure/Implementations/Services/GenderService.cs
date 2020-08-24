using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Factories.Implementations.Database;
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
        /// Обертка управления транзакциями
        /// </summary>
        private readonly BoutiqueDatabase _boutiqueDatabase;

        public GenderService(BoutiqueDatabase boutiqueDatabase)
        {
            _boutiqueDatabase = boutiqueDatabase;
        }

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task<IReadOnlyCollection<Gender>> GetGenders() =>
            await _boutiqueDatabase.Genders.ToListAsync().
            MapTaskAsync(genders => genders.Select(GenderEntityConverter.FromEntity).ToList().AsReadOnly());

        /// <summary>
        /// Загрузить типы пола для одежды в базу данных
        /// </summary>
        public async Task UploadGenders(IEnumerable<Gender> genders) =>
            await genders.Select(GenderEntityConverter.ToEntity).
            VoidAsync(genderEntitties => _boutiqueDatabase.Genders.AddRangeAsync(genderEntitties)).
            VoidBindAsync(_ => _boutiqueDatabase.SaveChangesAsync());
    }
}