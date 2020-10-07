using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных размеров одежды
    /// </summary>
    public class SizeTable : EntityDatabaseTable<(SizeType, int), SizeEntity>, ISizeTable
    {
        public SizeTable(DbSet<SizeEntity> sizeSet)
            : base(sizeSet)
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<SizeEntity, bool>> IdPredicate((SizeType, int) id) =>
            entity => entity.SizeType == id.Item1 && entity.SizeValue == id.Item2;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<SizeEntity, bool>> IdsPredicate(IEnumerable<(SizeType, int)> ids) =>
            entity => ids.Contains(new Tuple<SizeType, int>(entity.SizeType, entity.SizeValue).ToValueTuple());
    }
}