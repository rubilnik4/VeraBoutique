using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Connection;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Factories.Implementations.Database.Boutique
{
    /// <summary>
    /// Фабрика для создания базы данных
    /// </summary>
    public class BoutiqueDatabaseFactory: IBoutiqueDatabaseFactory
    {
        /// <summary>
        /// Параметры подключения к базе данных
        /// </summary>
        private readonly IResultValue<DatabaseConnection> _databaseConnection;

        public BoutiqueDatabaseFactory(IResultValue<DatabaseConnection> databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        /// <summary>
        /// База данных магазина
        /// </summary>
        private IResultValue<IBoutiqueDatabase>? _boutiqueDatabase;

        /// <summary>
        /// Получить базу данных магазина
        /// </summary>
        public IResultValue<IBoutiqueDatabase> BoutiqueDatabase =>
            _boutiqueDatabase ??=
            _databaseConnection.
            ResultValueOk(connection => new DbContextOptionsBuilder().UseNpgsql(connection.ConnectionString)).
            ResultValueOk(optionBuilder => new BoutiqueEntityDatabase(optionBuilder.Options));
    }
}