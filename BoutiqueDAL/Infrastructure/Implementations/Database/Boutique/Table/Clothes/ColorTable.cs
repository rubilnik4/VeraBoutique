using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных цвета одежды
    /// </summary>
    public class ColorTable : EntityDatabaseTable<string, IColorDomain, ColorEntity>, IColorTable
    {
        public ColorTable(DbSet<ColorEntity> colorClothesEntity)
            : base(colorClothesEntity)
        { }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<ColorEntity, string>> IdSelect() =>
            entity => entity.Name;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<ColorEntity, bool>> IdPredicate(string id) =>
            entity => entity.Name == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<ColorEntity, bool>> IdsPredicate(IEnumerable<string> ids) =>
            entity => ids.Contains(entity.Name);
    }
}