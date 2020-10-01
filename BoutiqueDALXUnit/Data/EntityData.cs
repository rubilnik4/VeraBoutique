﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Тестовые данные сущностей
    /// </summary>
    public static class EntityData
    {
        /// <summary>
        /// Получить сущности типа пола
        /// </summary>
        public static List<GenderEntity> GenderEntities =>
            GenderData.GetGendersDomain().
            Select(genderDomain => new GenderEntity(genderDomain.GenderType, genderDomain.Name)).
            ToList();

        /// <summary>
        /// Получить сущности вида одежды
        /// </summary>
        public static List<ClothesTypeEntity> ClothesTypeEntities =>
            ClothesTypeData.GetClothesTypeDomain().
            Select(clothesTypeDomain => new ClothesTypeEntity(clothesTypeDomain.Name)).
            ToList();

        /// <summary>
        /// Получить вид одежды
        /// </summary>
        public static List<GenderEntity> GetGenderEntitiesWithClothesType(IReadOnlyCollection<GenderEntity> genderEntities,
                                                                          IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            genderEntities.
            Select(gender => new GenderEntity(gender.GenderType, gender.Name,
                                                             GetClothesTypeGenderEntity(gender, clothesTypeEntities))).
            ToList();

        /// <summary>
        /// Получить пол с видом одежды
        /// </summary>
        public static IList<ClothesTypeGenderEntity> GetClothesTypeGenderEntity(GenderEntity genderEntity,
                                                                                IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities) =>
            clothesTypeEntities.
            Select(clothesTypeEntity => new ClothesTypeGenderEntity(clothesTypeEntity.Id, clothesTypeEntity, 
                                                                    genderEntity.Id, genderEntity)).
            ToList();

        /// <summary>
        /// Получить сущности для теста
        /// </summary>
        public static List<TestEntity> TestEntities =>
            TestData.GetTestDomains().
            Select(testDomain => new TestEntity(testDomain.TestEnum, testDomain.Name)).
            ToList();

        /// <summary>
        /// Тестовые сущности в результирующей коллекции
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntities =>
            new ResultCollection<TestEntity>(TestEntities);

        /// <summary>
        /// Пустая коллекция результирующих сущностей
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntitiesEmpty =>
           new ResultCollection<TestEntity>(Enumerable.Empty<TestEntity>());
    }
}