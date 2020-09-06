using BoutiqueCommon.Models.Interfaces.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Base
{
    /// <summary>
    /// Базовая модель базы данных
    /// </summary>
    public interface IEntityModel<TId>: IDomainModel<TId> 
        where TId : notnull
    { }
}