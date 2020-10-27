using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Services.ClothesType.Mocks;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType
{
    /// <summary>
    /// Сервис группы размеров одежды в базе данных. Тесты
    /// </summary>
    public class SizeGroupDatabaseServiceTest
    {
        /// <summary>
        /// Получить группу размеров совместно с размерами
        /// </summary>
        [Fact]
        public async Task GetSizeGroupsIncludeSize_Ok()
        {
            var sizeGroupInitial = SizeGroupData.GetSizeGroupDomain().First();
            var clothesSizeType = sizeGroupInitial.ClothesSizeType;
            int sizeNormalize = sizeGroupInitial.SizeNormalize;
            var sizeGroupEntities = SizeGroupEntitiesData.SizeGroupEntities;
            var sizeGroupTable = SizeGroupTableMock.GetSizeGroupTable(SizeGroupTableMock.SizeGroupOk(sizeGroupEntities));
            var sizeGroupEntityConverter = SizeGroupEntityConverter;
            var sizeDatabaseService = new SizeGroupDatabaseService(Database.Object, sizeGroupTable.Object,
                                                                   sizeGroupEntityConverter);

            var sizeGroupResults = await sizeDatabaseService.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);

            Assert.True(sizeGroupResults.OkStatus);
            Assert.True(sizeGroupResults.Value.Equals(sizeGroupInitial));
        }

        /// <summary>
        /// Получить группу размеров совместно с размерами. Ошибка
        /// </summary>
        [Fact]
        public async Task GetSizeGroupsIncludeSize_Exception()
        {
            var sizeGroupInitial = SizeGroupData.GetSizeGroupDomain().First();
            var clothesSizeType = sizeGroupInitial.ClothesSizeType;
            int sizeNormalize = sizeGroupInitial.SizeNormalize;
            var sizeGroupTable = SizeGroupTableMock.GetSizeGroupTable(SizeGroupTableMock.SizeGroupException());
            var sizeGroupEntityConverter = SizeGroupEntityConverter;
            var sizeDatabaseService = new SizeGroupDatabaseService(Database.Object, sizeGroupTable.Object,
                                                                   sizeGroupEntityConverter);

            var sizeGroupResults = await sizeDatabaseService.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);

            Assert.True(sizeGroupResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, sizeGroupResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить группу размеров совместно с размерами. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetSizeGroupsIncludeSize_NotFound()
        {
            var sizeGroupInitial = SizeGroupData.GetSizeGroupDomain().First();
            var clothesSizeType = sizeGroupInitial.ClothesSizeType;
            int sizeNormalize = sizeGroupInitial.SizeNormalize;
            var sizeGroupTable = SizeGroupTableMock.GetSizeGroupTable(SizeGroupTableMock.SizeGroupNotFound());
            var sizeGroupEntityConverter = SizeGroupEntityConverter;
            var sizeDatabaseService = new SizeGroupDatabaseService(Database.Object, sizeGroupTable.Object,
                                                                   sizeGroupEntityConverter);

            var sizeGroupResults = await sizeDatabaseService.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);

            Assert.True(sizeGroupResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, sizeGroupResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database => new Mock<IDatabase>();

        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        private static ISizeGroupEntityConverter SizeGroupEntityConverter =>
            new SizeGroupEntityConverter(new SizeEntityConverter());
    }
}