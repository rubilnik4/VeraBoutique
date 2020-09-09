using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Base
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Тесты
    /// </summary>
    public class EntityDatabaseTableTest
    {
        /// <summary>
        /// Добавить сущности в таблицу. Проверить идентификаторы
        /// </summary>
        [Fact]
        public async Task AddRange_CheckEntities()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();
            var genderDatabaseTable = boutiqueDatabase.GendersTable;
            var genders = EntityData.GetGenderEntities();

            var ids = await genderDatabaseTable.AddRangeAsync(genders);
            var result = await boutiqueDatabase.SaveChangesAsync();

            Assert.True(result.OkStatus);
            Assert.True(ids.OkStatus);
            Assert.True(ids.Value.SequenceEqual(genders.Select(gender => gender.GenderType)));
        }

        /// <summary>
        /// Добавить одинаковые данные дважды. Ошибка
        /// </summary>
        [Fact]
        public async Task AddRange_DuplicateError()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();
            var genderDatabaseTable = boutiqueDatabase.GendersTable;
            var genders = EntityData.GetGenderEntities();

            await genderDatabaseTable.AddRangeAsync(genders);
            var firstResult = await boutiqueDatabase.SaveChangesAsync();

            await genderDatabaseTable.AddRangeAsync(genders);
            var secondResult = await boutiqueDatabase.SaveChangesAsync();

            Assert.True(firstResult.OkStatus);
            Assert.True(secondResult.HasErrors);
            Assert.True(secondResult.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Получить сущности из таблицы
        /// </summary>
        [Fact]
        public async Task AddRange_GetEntities()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();
            var genderDatabaseTable = boutiqueDatabase.GendersTable;
            var genders = EntityData.GetGenderEntities();

            await genderDatabaseTable.AddRangeAsync(genders);
            var result = await boutiqueDatabase.SaveChangesAsync();
            var gendersGet = await genderDatabaseTable.ToListAsync();

            Assert.True(result.OkStatus);
            Assert.True(gendersGet.OkStatus);
            Assert.True(genders.SequenceEqual(gendersGet.Value));
        }

        /// <summary>
        /// Добавить сущности в таблицу. Получить вторую
        /// </summary>
        [Fact]
        public async Task AddRange_GetSecond()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();
           
            var genderDatabaseTable = boutiqueDatabase.GendersTable;
            var genders = EntityData.GetGenderEntities();

            var ids = await genderDatabaseTable.AddRangeAsync(genders);
            var result = await boutiqueDatabase.SaveChangesAsync();
            var getId = ids.Value.Last();
            var genderGet = await genderDatabaseTable.FirstAsync(getId);

            Assert.True(result.OkStatus);
            Assert.True(genderGet.OkStatus);
            Assert.True(genderGet.Value.Equals(genders.First(gender => gender.GenderType == getId)));
        }

        /// <summary>
        /// Добавить сущности в таблицу. Элемент не найден
        /// </summary>
        [Fact]
        public async Task AddRange_GetNotFount()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();

            var genderDatabaseTable = boutiqueDatabase.GendersTable;
            var genders = EntityData.GetGenderEntities();

            var ids = await genderDatabaseTable.AddRangeAsync(genders);
            var result = await boutiqueDatabase.SaveChangesAsync();
            var genderGet = await genderDatabaseTable.FirstAsync(GenderType.Child);

            Assert.True(result.OkStatus);
            Assert.True(genderGet.HasErrors);
            Assert.True(genderGet.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Получить вторую
        /// </summary>
        [Fact]
        public async Task AddRange_Update()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();

            var genderDatabaseTable = boutiqueDatabase.GendersTable;
            var genders = EntityData.GetGenderEntities();
            var genderUpdate = new GenderEntity(GenderType.Female, "Мужа");

            await genderDatabaseTable.AddRangeAsync(genders);
            var resultSave = await boutiqueDatabase.SaveChangesAsync();
            
            var resultUpdate = genderDatabaseTable.Update(genderUpdate);
            var resultAfterUpdate = await boutiqueDatabase.SaveChangesAsync();

            Assert.True(resultSave.OkStatus);
            Assert.True(resultUpdate.OkStatus);
            Assert.True(resultAfterUpdate.OkStatus);
        }

        /// <summary>
        /// База данных в памяти
        /// </summary>
        private static BoutiqueEntityDatabase GetBoutiqueDatabase() =>
            new BoutiqueEntityDatabase(GetBoutiqueDatabaseOptions());

        /// <summary>
        /// Параметры подключения к базе
        /// </summary>
        private static DbContextOptions GetBoutiqueDatabaseOptions() =>
            new DbContextOptionsBuilder().
                UseInMemoryDatabase(Guid.NewGuid().ToString()).
                Options;
    }
}