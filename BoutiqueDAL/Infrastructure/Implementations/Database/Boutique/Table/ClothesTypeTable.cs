using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных вида одежды
    /// </summary>
    public class ClothesTypeTable : EntityDatabaseTable<string, ClothesTypeEntity>, IClothesTypeTable
    {
        public ClothesTypeTable(DbSet<ClothesTypeEntity> clothesTypeSet)
         : base(clothesTypeSet)
        {
            _clothesTypeSet = clothesTypeSet;
        }

        /// <summary>
        /// Таблица типа пола
        /// </summary>
        private readonly DbSet<ClothesTypeEntity> _clothesTypeSet;

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> Where(IEnumerable<string> ids) =>
            _clothesTypeSet.Where(clothesTypeEntity => ids.Contains(clothesTypeEntity.Name));

        /// <summary>
        /// Поиск по параметрам с включением сущностей
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> Where<TIdOut, TEntityOut>(IEnumerable<string> ids, 
                                                                                   Func<ClothesTypeEntity, IReadOnlyCollection<TEntityOut>> include)=>
            _clothesTypeSet.
            Include(clothesTypeEntity => include(clothesTypeEntity)).
            Where(clothesTypeEntity => ids.Contains(clothesTypeEntity.Name));
    }
}