using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных вида одежды
    /// </summary>
    public class ClothesTypeTable : EntityDatabaseTable<string, ClothesTypeFullEntity>, IClothesTypeTable
    {
        public ClothesTypeTable(DbSet<ClothesTypeFullEntity> clothesTypeSet)
         : base(clothesTypeSet)
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<ClothesTypeFullEntity, bool>> IdPredicate(string id) =>
            entity => entity.Name == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<ClothesTypeFullEntity, bool>> IdsPredicate(IEnumerable<string> ids) =>
            entity => ids.Contains(entity.Name);
    }
}