using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Factories.Interfaces.Database.Boutique
{
    /// <summary>
    /// Фабрика для создания базы данных
    /// </summary>
    public interface IBoutiqueDatabaseFactory
    {
        /// <summary>
        /// Получить базу данных магазина
        /// </summary>
        IResultValue<IBoutiqueDatabase> BoutiqueDatabase { get; }
    }
}