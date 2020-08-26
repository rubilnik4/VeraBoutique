namespace BoutiqueDAL.Entities.Base
{
    /// <summary>
    /// Идентичность сущностей
    /// </summary>
    public interface IEqualEntity<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Идентичны ли сущности
        /// </summary>
        bool EqualEntity(TEntity entity);
    }
}