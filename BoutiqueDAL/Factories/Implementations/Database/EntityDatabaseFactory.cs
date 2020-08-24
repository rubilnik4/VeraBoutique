using BoutiqueDAL.Factories.Interfaces.Database;
using BoutiqueDAL.Models.Implementations.Connection;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Factories.Implementations.Database
{
    /// <summary>
    /// Фабрика для создания базы данных
    /// </summary>
    public class EntityDatabaseFactory: IDatabaseFactory
    {
        /// <summary>
        /// Параметры подключения к базе данных
        /// </summary>
        private readonly IResultValue<DatabaseConnection> _databaseConnection;

        public EntityDatabaseFactory(IResultValue<DatabaseConnection> databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        /// <summary>
        /// База данных
        /// </summary>
        private IResultValue<BoutiqueDatabase>? _boutiqueDatabase = null;

        /// <summary>
        /// Получить базу данных
        /// </summary>
        public IResultValue<BoutiqueDatabase> GetDatabase(IResultValue<DatabaseConnection> databaseConnection) =>
            _boutiqueDatabase ??=
            databaseConnection.
            ResultValueOk(connection => new DbContextOptionsBuilder().UseNpgsql(connection.ConnectionString)).
            ResultValueOk(optionBuilder => new BoutiqueDatabase(optionBuilder.Options));
    }
}