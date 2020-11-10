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
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных типа пола
    /// </summary>
    public class GenderTable : EntityDatabaseTable<GenderType, IGenderDomain, GenderEntity>, IGenderTable
    {
        public GenderTable(DbSet<GenderEntity> genderSet)
            : base(genderSet)
        { }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<GenderEntity, GenderType>> IdSelect() =>
            entity => entity.GenderType;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<GenderEntity, bool>> IdPredicate(GenderType id) =>
            entity => entity.GenderType == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<GenderEntity, bool>> IdsPredicate(IEnumerable<GenderType> ids) =>
            entity => ids.Contains(entity.GenderType);
    }
}