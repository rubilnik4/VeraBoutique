using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
    }
}