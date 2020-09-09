using System;
using System.Linq.Expressions;

namespace BoutiqueCommon.Models.Common.Interfaces.Base
{
    /// <summary>
    /// Базовая модель
    /// </summary>
    public interface IModel<out TId> 
        where TId: notnull
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        TId Id{ get; }
    }
}