using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Owns;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    public class ClothesImageTable : EntityDatabaseTable<int, IClothesImageDomain, ClothesImageEntity>, IClothesImageTable
    {
        public ClothesImageTable(DbSet<ClothesImageEntity> clothesImageSet)
            : base(clothesImageSet)
        { }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<ClothesImageEntity, int>> IdSelect() =>
            entity => entity.Id;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<ClothesImageEntity, bool>> IdPredicate(int id) =>
            entity => id == entity.Id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<ClothesImageEntity, bool>> IdsPredicate(IEnumerable<int> ids) =>
            entity => ids.Contains(entity.Id);
    }
}