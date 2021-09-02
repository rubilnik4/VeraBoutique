using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы группы размера одежды. Тесты
    /// </summary>
    public class SizeGroupDatabaseValidateServiceTest: SizeGroupDatabaseValidateService
    {
        public SizeGroupDatabaseValidateServiceTest()
            :base(SizeGroupTable.Object, 
                  SizeDatabaseValidateServiceMock.GetSizeDatabaseValidateService(SizeEntitiesData.SizeEntities))
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var sizeGroup = SizeGroupData.SizeGroupMainDomains.First();

            var result = ValidateModel(sizeGroup);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель. Ошибка имени
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var sizeGroup = SizeGroupData.SizeGroupMainDomains.First();
            var sizeGroupSizeNormalize = new SizeGroupMainDomain(ClothesSizeType.Dress, 0, sizeGroup.Sizes);

            var result = ValidateModel(sizeGroupSizeNormalize);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить модель. Ошибка размеров
        /// </summary>
        [Fact]
        public void ValidateModel_SizesError()
        {
            var sizeGroup = SizeGroupData.SizeGroupMainDomains.First();
            var sizeGroupEmptySizes = new SizeGroupMainDomain(sizeGroup, Enumerable.Empty<ISizeDomain>());

            var result = ValidateModel(sizeGroupEmptySizes);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_Ok()
        {
            var sizeGroup = SizeGroupData.SizeGroupMainDomains.First();

            var result = await ValidateIncludes(sizeGroup);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Размеры не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_SizesNotFound()
        {
            var sizes = SizeData.SizeDomains.Append(new SizeDomain(SizeType.American, "NotFound"));
            var sizeGroup = SizeGroupData.SizeGroupMainDomains.First();
            var sizeGroupNotFound = new SizeGroupMainDomain(sizeGroup, sizes);

            var result = await ValidateIncludes(sizeGroupNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_Ok()
        {
            var sizeGroups = SizeGroupData.SizeGroupMainDomains.
                             OrderByDescending(sizeGroup => sizeGroup.SizeNormalize);

            var result = await ValidateIncludes(sizeGroups);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Размеры не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_SizesNotFound()
        {
            var sizes = SizeData.SizeDomains.Append(new SizeDomain(SizeType.American, "NotFound"));
            var sizeGroup = SizeGroupData.SizeGroupMainDomains.First();
            var sizeGroupsNotFound = SizeGroupData.SizeGroupMainDomains.Append(new SizeGroupMainDomain(sizeGroup, sizes));

            var result = await ValidateIncludes(sizeGroupsNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        private static Mock<ISizeGroupTable> SizeGroupTable =>
            new ();
    }
}