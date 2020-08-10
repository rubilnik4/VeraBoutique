using System;

namespace BoutiqueDAL.Entities.Base
{
    /// <summary>
    /// Базовый класс для сущностей
    /// </summary>
    public abstract class BaseEntity<TIdType> where TIdType : IEquatable<TIdType>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public abstract TIdType Id { get; protected set; }
    }
}