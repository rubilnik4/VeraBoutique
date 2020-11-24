using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
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
        { }

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
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        protected override Expression<Func<ClothesTypeEntity, bool>> ValidateQuery(IQueryable<ClothesTypeEntity> entities,
                                                                                   IClothesTypeDomain domain) =>
           clothesType => domain.Name == clothesType.Name &&
                          domain.Category.Name == clothesType.CategoryName;

        /// <summary>
        /// Включение сущностей при загрузке полных данных
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> GetInclude(IQueryable<ClothesTypeEntity> entities) =>
            entities.Include(entity => entity.Category).
                     Include(entity => entity.ClothesTypeGenderComposites);

        /// <summary>
        /// Функция для проверки наличия
        /// </summary>
        protected override Expression<Func<ClothesTypeEntity, bool>> ValidateQuery(IQueryable<ClothesTypeEntity> entities,
                                                                                   IReadOnlyCollection<IClothesTypeDomain> domains) =>
           clothesType => domains.Select(domain => domain.Name).Contains(clothesType.Name) &&
                          domains.Select(domain => domain.Category.Name).Contains(clothesType.CategoryName);

        /// <summary>
        /// Функция проверки наличия вложенных сущностей
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> ValidateInclude(IQueryable<ClothesTypeEntity> entities, 
                                                                         IReadOnlyCollection<IClothesTypeDomain> domains) =>
            entities.
            Include(clothesType => clothesType.ClothesTypeGenderComposites).
            Where(clothesType => clothesType.ClothesTypeGenderComposites.
                                 Select(clothesTypeGender => clothesTypeGender.GenderType).
                                 SequenceEqual(domains.First(domain => domain.Name == clothesType.Name).
                                               Genders.Select(gender => gender.GenderType)));
    }
}