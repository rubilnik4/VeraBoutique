using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных одежды
    /// </summary>
    public class ClothesTable : EntityDatabaseTable<int, IClothesFullDomain, ClothesFullEntity>, IClothesTable
    {
        public ClothesTable(DbSet<ClothesFullEntity> clothesSet)
            : base(clothesSet)
        {
            _clothesSet = clothesSet;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<ClothesFullEntity> _clothesSet;

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<ClothesFullEntity, int>> IdSelect() =>
            entity => entity.Id;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<ClothesFullEntity, bool>> IdPredicate(int id) =>
            entity => entity.Id == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<ClothesFullEntity, bool>> IdsPredicate(IEnumerable<int> ids) =>
            entity => ids.Contains(entity.Id);

        /// <summary>
        /// Включение сущностей при загрузке полных данных
        /// </summary>
        protected override IQueryable<ClothesFullEntity> EntitiesIncludes =>
            _clothesSet.Include(entity => entity.Gender).
                        Include(entity => entity.ClothesType).
                        Include(entity => entity.ClothesColorComposites).
                        ThenInclude(composite => composite.ColorClothes).
                        Include(entity => entity.ClothesSizeGroupComposites).
                        ThenInclude(composite => composite.SizeGroup).
                        ThenInclude(composite => composite!.SizeGroupComposites).
                        ThenInclude(composite => composite.Size);
    }
}