using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    public class ClothesImageTable : EntityDatabaseTable<Guid, IClothesImageDomain, ClothesImageEntity>, IClothesImageTable
    {
        public ClothesImageTable(DbSet<ClothesImageEntity> clothesImageSet)
            : base(clothesImageSet)
        { }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<ClothesImageEntity, Guid>> IdSelect() =>
            entity => entity.Id;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<ClothesImageEntity, bool>> IdPredicate(Guid id) =>
            entity => id == entity.Id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<ClothesImageEntity, bool>> IdsPredicate(IEnumerable<Guid> ids) =>
            entity => ids.Contains(entity.Id);
    }
}