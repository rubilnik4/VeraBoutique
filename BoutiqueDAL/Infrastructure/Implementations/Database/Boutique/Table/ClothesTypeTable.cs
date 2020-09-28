using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        /// Поиск первого с включением сущностей
        /// </summary>
        protected override async Task<ClothesTypeEntity?> FirstAsync<TIdOut, TEntityOut>(string id,
                                                                                         Expression<Func<ClothesTypeEntity, IEnumerable<TEntityOut>>> include) =>
            await _clothesTypeSet.
                Include(include).
                FirstOrDefaultAsync(clothesTypeEntity => clothesTypeEntity.Name == id);

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> Where(IEnumerable<string> ids) =>
            _clothesTypeSet.Where(clothesTypeEntity => ids.Contains(clothesTypeEntity.Name));

        /// <summary>
        /// Поиск по параметрам с включением сущностей
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> Where<TIdOut, TEntityOut>(IEnumerable<string> ids,
                                                                                   Expression<Func<ClothesTypeEntity, IEnumerable<TEntityOut>>> include)=>
            _clothesTypeSet.
            Include(include).
            Where(clothesTypeEntity => ids.Contains(clothesTypeEntity.Name));
    }
}