﻿using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных типа пола. Тесты
    /// </summary>
    public class GenderTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var gender = GenderEntitiesData.GenderEntities.First();
            var genderTable = GenderTable;

            var id = genderTable.IdSelect().Compile()(gender);

            Assert.Equal(gender.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var gender = GenderEntitiesData.GenderEntities.First();
            var genderTable = GenderTable;

            bool isFound = genderTable.IdPredicate(gender.Id).Compile()(gender);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var genders = GenderEntitiesData.GenderEntities;
            var genderTable = GenderTable;

            bool isFound = genderTable.IdsPredicate(genders.Select(gender => gender.Id)).
                                       Compile()(genders.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateValueFilter()
        {
            var genderDomain = GenderData.GendersDomain.First();
            var genders = GenderEntitiesData.GenderEntities.AsQueryable();
            var genderTable = GenderTable;
            var genderEntityConverter = GenderEntityConverterMock.GenderEntityConverter;

            var entities = genderTable.ValidateFilter(genders, genderDomain);
            var domains = genderEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(1, domains.Value.Count);
            Assert.True(genderDomain.Equals(domains.Value.First()));
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateCollectionFilter()
        {
            var genderDomains = GenderData.GendersDomain;
            var genders = GenderEntitiesData.GenderEntities.AsQueryable();
            var genderTable = GenderTable;
            var genderEntityConverter = GenderEntityConverterMock.GenderEntityConverter;


            var entities = genderTable.ValidateFilter(genders, genderDomains);
            var domains = genderEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(genderDomains.Count, domains.Value.Count);
            Assert.True(genderDomains.SequenceEqual(domains.Value));
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<GenderEntity>> DbSet =>
            new Mock<DbSet<GenderEntity>>();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static IGenderTable GenderTable =>
            new GenderTable(DbSet.Object);
    }
}