using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Поиск в моделях
    /// </summary>
    public static class SearchInEntities
    {
        /// <summary>
        /// Найти сущность
        /// </summary>
        public static TestEntity FirstEntity(IEnumerable<TestEntity> entities, TestEnum id) =>
            entities.First(entity => entity.Id == id);

        /// <summary>
        /// Найти сущности
        /// </summary>
        public static IEnumerable<TestEntity> FindEntities(IEnumerable<TestEntity> entities, IEnumerable<TestEnum> ids) =>
            ids.ToList().
            Map(idsCollection => entities.Where(entity => idsCollection.Contains(entity.Id)));
    }
}