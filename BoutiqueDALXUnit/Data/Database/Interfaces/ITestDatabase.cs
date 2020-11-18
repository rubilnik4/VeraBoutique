using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Models.Implementation;

namespace BoutiqueDALXUnit.Data.Database.Interfaces
{
    /// <summary>
    /// Тестовая база данных
    /// </summary>
    public interface ITestDatabase : IDatabase
    {
        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        ITestTable TestTable { get; }
    }
}