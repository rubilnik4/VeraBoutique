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
        public override IQueryable<ClothesTypeEntity> ValidateFilter(IQueryable<ClothesTypeEntity> entities, IClothesTypeDomain domain) =>
           entities.Where(clothesType => domain.Id == clothesType.Name &&
                                         domain.Category.Name == clothesType.CategoryName).
           Map(clothesTypes => ClothesTypeGenderValidate(clothesTypes, 
                                                         new List<IClothesTypeDomain> { domain }));

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        public override IQueryable<ClothesTypeEntity> ValidateFilter(IQueryable<ClothesTypeEntity> entities,
                                                                     IReadOnlyCollection<IClothesTypeDomain> domains) =>
           entities.
           Where(clothesType => domains.Select(domain => domain.Id).Contains(clothesType.Name) &&
                                domains.Select(domain => domain.Category.Name).Contains(clothesType.CategoryName)).
           Map(clothesTypes => ClothesTypeGenderValidate(clothesTypes, domains));

        /// <summary>
        /// Проверить наличие связующей сущности вида одежды и типа пола
        /// </summary>
        private static IQueryable<ClothesTypeEntity> ClothesTypeGenderValidate(IQueryable<ClothesTypeEntity> entities,
                                                                               IReadOnlyCollection<IClothesTypeDomain> domains) =>
            entities.
            Include(clothesType => clothesType.ClothesTypeGenderComposites).
            Where(entity => entity.ClothesTypeGenderComposites.
                            Select(clothesTypeGender => clothesTypeGender.GenderType).
                            SequenceEqual(domains.First(domain => domain.Id == entity.Id).
                                          Genders.Select(gender => gender.GenderType)));
    }
}