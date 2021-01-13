using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных размеров одежды
    /// </summary>
    public class SizeTable : EntityDatabaseTable<(SizeType SizeType, string Name), ISizeDomain, SizeEntity>, ISizeTable
    {
        public SizeTable(DbSet<SizeEntity> sizeSet)
            : base(sizeSet)
        { }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<SizeEntity, (SizeType, string)>> IdSelect() =>
            entity => new Tuple<SizeType, string>(entity.SizeType, entity.Name).ToValueTuple();

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public  override Expression<Func<SizeEntity, bool>> IdPredicate((SizeType SizeType, string Name) id) =>
            entity => id.SizeType == entity.SizeType && id.Name == entity.Name;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<SizeEntity, bool>> IdsPredicate(IEnumerable<(SizeType SizeType, string Name)> ids) =>
            entity => ids.Select(id => id.SizeType + id.Name).
                          Contains(entity.SizeType + entity.Name);
    }
}