using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных размеров одежды
    /// </summary>
    public class SizeTable : EntityDatabaseTable<(SizeType, string), SizeEntity>, ISizeTable
    {
        public SizeTable(DbSet<SizeEntity> sizeSet)
            : base(sizeSet)
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<SizeEntity, bool>> IdPredicate((SizeType, string) id) =>
            entity => entity.SizeType == id.Item1 && entity.SizeName == id.Item2;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<SizeEntity, bool>> IdsPredicate(IEnumerable<(SizeType, string)> ids) =>
            entity => ids.Contains(new Tuple<SizeType, string>(entity.SizeType, entity.SizeName).ToValueTuple());
    }
}