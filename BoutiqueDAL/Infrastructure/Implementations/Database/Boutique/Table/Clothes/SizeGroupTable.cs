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
    /// Таблица базы данных группы размеров одежды
    /// </summary>
    public class SizeGroupTable : EntityDatabaseTable<(ClothesSizeType, int), SizeGroupEntity>, ISizeGroupTable
    {
        public SizeGroupTable(DbSet<SizeGroupEntity> sizeGroupSet)
            : base(sizeGroupSet)
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<SizeGroupEntity, bool>> IdPredicate((ClothesSizeType, int) id) =>
            entity => entity.ClothesSizeType == id.Item1 && entity.SizeNormalize == id.Item2;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<SizeGroupEntity, bool>> IdsPredicate(IEnumerable<(ClothesSizeType, int)> ids) =>
            entity => ids.Contains(new Tuple<ClothesSizeType, int>(entity.ClothesSizeType, entity.SizeNormalize).ToValueTuple());
    }
}