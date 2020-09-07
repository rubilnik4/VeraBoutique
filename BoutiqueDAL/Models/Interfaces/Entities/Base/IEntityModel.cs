using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Base
{
    /// <summary>
    /// Базовая модель базы данных
    /// </summary>
    public interface IEntityModel<TId>: IModel<TId> 
        where TId : notnull
    { }
}