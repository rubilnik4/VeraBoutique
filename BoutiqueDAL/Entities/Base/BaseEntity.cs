using System;

namespace BoutiqueDAL.Entities.Base
{
    /// <summary>
    /// Базовый класс для сущностей
    /// </summary>
    public abstract class BaseEntity<TId>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public abstract TId Id { get; }
    }
}