using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Carts
{
    /// <summary>
    /// Таблица базы данных корзин
    /// </summary>
    public class CartTable : EntityDatabaseTable<Guid, ICartDomain, CartEntity>, ICartTable
    {
        public CartTable(DbSet<CartEntity> cartSet)
           : base(cartSet)
        {
            _cartSet = cartSet;
        }

        /// <summary>
        /// Экземпляр таблицы базы данных
        /// </summary>
        private readonly DbSet<CartEntity> _cartSet;

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<CartEntity, Guid>> IdSelect() =>
            entity => entity.Id;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<CartEntity, bool>> IdPredicate(Guid id) =>
            entity => entity.Id == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<CartEntity, bool>> IdsPredicate(IEnumerable<Guid> ids) =>
            entity => ids.Contains(entity.Id);

        /// <summary>
        /// Включение сущностей при загрузке полных данных
        /// </summary>
        protected override IQueryable<CartEntity> EntitiesIncludes =>
            _cartSet.Include(entity => entity.CartItems);

        /// <summary>
        /// Включение сущностей при загрузке полных данных
        /// </summary>
        protected override IQueryable<CartEntity> EntitiesIncludesDelete =>
            _cartSet.Include(entity => entity.CartItems);
    }
}