using System;
using System.Collections;
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
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<GenderEntity, bool>> IdPredicate(GenderType id) =>
            entity => entity.GenderType == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<GenderEntity, bool>> IdsPredicate(IEnumerable<GenderType> ids) =>
            entity => ids.Contains(entity.GenderType);
    }
}