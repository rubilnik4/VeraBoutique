using System;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueDAL.Models.Interfaces.Entities.Base
{
    /// <summary>
    /// Базовая модель базы данных
    /// </summary>
    public interface IEntityModel<out TId> : IModel<TId>
        where TId : notnull
    { }
}
