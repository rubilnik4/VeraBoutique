using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    public class ClothesTable : EntityDatabaseTable<int, ClothesEntity>, IClothesTable
    {
        public ClothesTable(DbSet<ClothesEntity> clothesInformationSet)
            : base(clothesInformationSet)
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<ClothesEntity, bool>> IdPredicate(int id) =>
            entity => entity.Id == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<ClothesEntity, bool>> IdsPredicate(IEnumerable<int> ids) =>
            entity => ids.Contains(entity.Id);
    }
}