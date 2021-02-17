using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных вида одежды
    /// </summary>
    public class ClothesTypeTable : EntityDatabaseTable<string, IClothesTypeDomain, ClothesTypeEntity>, IClothesTypeTable
    {
        public ClothesTypeTable(DbSet<ClothesTypeEntity> clothesTypeSet)
         : base(clothesTypeSet)
        {
            _clothesTypeSet = clothesTypeSet;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<ClothesTypeEntity> _clothesTypeSet;

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<ClothesTypeEntity, string>> IdSelect() =>
            entity => entity.Name;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<ClothesTypeEntity, bool>> IdPredicate(string id) =>
            clothesType => clothesType.Name == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<ClothesTypeEntity, bool>> IdsPredicate(IEnumerable<string> ids) =>
            clothesType => ids.Contains(clothesType.Name);

        /// <summary>
        /// Включение сущностей при загрузке полных данных
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> EntitiesIncludes =>
            _clothesTypeSet.Include(entity => entity.Category).
                            Include(entity => entity.ClothesTypeGenderComposites).
                            ThenInclude(composite => composite.Gender);
    }
}