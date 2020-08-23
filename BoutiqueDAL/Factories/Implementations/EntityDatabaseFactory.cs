using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Models.Implementations.Connection;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Factories.Implementations
{
    /// <summary>
    /// Фабрика для создания базы данных
    /// </summary>
    public class EntityDatabaseFactory: IDatabaseFactory
    {
        public EntityDatabaseFactory()
        {

        }



        public IResultValue<BoutiqueDatabase> GetDatabase(IResultValue<DatabaseConnection> databaseConnection) =>
            databaseConnection.
            ResultValueOk(connection => new DbContextOptionsBuilder().UseNpgsql(connection.ConnectionString)).
            ResultValueOk(optionBuilder => new BoutiqueDatabase(optionBuilder.Options));
    }
}