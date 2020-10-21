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
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType
{
    using SizeGroupTableExpression = Expression<Func<ISizeGroupTable, Task<IResultValue<SizeGroupEntity>>>>;
    using SizeGroupCompositeExpression = Expression<Func<SizeGroupEntity, IEnumerable<SizeGroupCompositeEntity>>>;

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
            var sizeGroupEntities = EntityData.SizeGroupEntities;
            var sizeGroupTable = GetSizeGroupTable(FirstResultOk(sizeGroupEntities));
            var sizeGroupEntityConverter = SizeGroupEntityConverter;
            var sizeDatabaseService = new SizeGroupDatabaseService(Database.Object, sizeGroupTable.Object,
                                                                   sizeGroupEntityConverter, QueryableService.Object);

            var sizeGroupResults = await sizeDatabaseService.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);

            Assert.True(sizeGroupResults.OkStatus);
            Assert.True(sizeGroupResults.Value.Equals(sizeGroupInitial));
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
            var sizeGroupTable = GetSizeGroupTable(FirstResultNotFound());
            var sizeGroupEntityConverter = SizeGroupEntityConverter;
            var sizeDatabaseService = new SizeGroupDatabaseService(Database.Object, sizeGroupTable.Object,
                                                                   sizeGroupEntityConverter, QueryableService.Object);

            var sizeGroupResults = await sizeDatabaseService.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);

            Assert.True(sizeGroupResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, sizeGroupResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database =>
            new Mock<IDatabase>();

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        private static Mock<ISizeGroupTable> GetSizeGroupTable(Func<(ClothesSizeType, int), IQueryable<SizeGroupEntity>> firstResultFunc) =>
            new Mock<ISizeGroupTable>().
            Void(mock => mock.Setup(sizeGroupTable => sizeGroupTable.Where(It.IsAny<(ClothesSizeType, int)>())).
                              Returns(firstResultFunc));

        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        private static ISizeGroupEntityConverter SizeGroupEntityConverter =>
            new SizeGroupEntityConverter(new SizeEntityConverter());

        /// <summary>
        /// Функция поиска группы размеров
        /// </summary>
        private static Func<(ClothesSizeType, int), IQueryable<SizeGroupEntity>> FirstResultOk(IEnumerable<SizeGroupEntity> sizeGroupEntities) =>
            sizeGroupId => sizeGroupEntities.Where(sizeGroup => sizeGroup.Id == sizeGroupId).AsQueryable();

        /// <summary>
        /// Функция поиска группы размеров. Элемент не найден
        /// </summary>
        private static Func<(ClothesSizeType, int), IQueryable<SizeGroupEntity>> FirstResultNotFound() =>
            _ => Enumerable.Empty<SizeGroupEntity>().AsQueryable();

        /// <summary>
        /// Сервис обработки запросов базы данных
        /// </summary>
        private static Mock<IQueryableService<(ClothesSizeType, int), SizeGroupEntity>> QueryableService =>
            new Mock<IQueryableService<(ClothesSizeType, int), SizeGroupEntity>>().
                Void(mock => mock.Setup(service => service.FirstOrDefaultAsync(It.IsAny<IEnumerable<SizeGroupEntity>>())).
                                  ReturnsAsync((IEnumerable<SizeGroupEntity> sizeGroupEntities) => sizeGroupEntities.FirstOrDefault()));
    }
}