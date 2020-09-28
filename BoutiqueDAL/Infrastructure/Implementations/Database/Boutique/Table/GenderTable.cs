using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных типа пола
    /// </summary>
    public class GenderTable : EntityDatabaseTable<GenderType, GenderEntity>, IGenderTable
    {
        public GenderTable(DbSet<GenderEntity> genderSet)
            : base(genderSet)
        {
            _genderSet = genderSet;
        }

        /// <summary>
        /// Таблица типа пола
        /// </summary>
        private readonly DbSet<GenderEntity> _genderSet;

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        protected override IQueryable<GenderEntity> Where(IEnumerable<GenderType> ids) =>
            _genderSet.Where(genderEntity => ids.Contains(genderEntity.GenderType));

        /// <summary>
        /// Поиск по параметрам с включением сущностей
        /// </summary>
        protected override IQueryable<GenderEntity> Where<TIdOut, TEntityOut>(IEnumerable<GenderType> ids,
                                                                              Func<GenderEntity, IReadOnlyCollection<TEntityOut>> include) =>
            _genderSet.
            Include(genderEntity => include(genderEntity)).
            Where(genderEntity => ids.Contains(genderEntity.GenderType));

        /// <summary>
        /// Поиск первого с включением сущностей
        /// </summary>
        protected override async Task<GenderEntity?> FirstAsync<TEntityOut>(GenderType id,
                                                                Expression<Func<GenderEntity, IEnumerable<TEntityOut>>> include) =>
            await _genderSet.
            Include(include).
            FirstOrDefaultAsync(genderEntity => genderEntity.GenderType == id);

        private static Expression<Func<GenderEntity, IEnumerable<ClothesTypeGenderEntity>>> Ttt =>
            genderEntity => genderEntity.ClothesTypeGenderEntities;
    }
}