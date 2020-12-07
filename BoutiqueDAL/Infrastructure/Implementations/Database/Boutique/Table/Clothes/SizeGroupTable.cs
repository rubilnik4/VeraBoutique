﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных группы размеров одежды
    /// </summary>
    public class SizeGroupTable : EntityDatabaseTable<(ClothesSizeType, int), ISizeGroupDomain, SizeGroupEntity>, ISizeGroupTable
    {
        public SizeGroupTable(DbSet<SizeGroupEntity> sizeGroupSet)
            : base(sizeGroupSet)
        {
            _sizeGroupSet = sizeGroupSet;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<SizeGroupEntity> _sizeGroupSet;

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<SizeGroupEntity, (ClothesSizeType, int)>> IdSelect() =>
            entity => new Tuple<ClothesSizeType, int>(entity.ClothesSizeType, entity.SizeNormalize).ToValueTuple();

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<SizeGroupEntity, bool>> IdPredicate((ClothesSizeType, int) id) =>
            entity => entity.ClothesSizeType == id.Item1 && entity.SizeNormalize == id.Item2;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<SizeGroupEntity, bool>> IdsPredicate(IEnumerable<(ClothesSizeType, int)> ids) =>
            entity => ids.Contains(new Tuple<ClothesSizeType, int>(entity.ClothesSizeType, entity.SizeNormalize).ToValueTuple());

        /// <summary>
        /// Включение сущностей при загрузке полных данных
        /// </summary>
        protected override IQueryable<SizeGroupEntity> EntitiesIncludes =>
            _sizeGroupSet.Include(entity => entity.SizeGroupComposites).
                     ThenInclude(composite => composite.Size);
    }
}