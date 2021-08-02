using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate;
using Functional.Models.Enums;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы изображения одежды. Тесты
    /// </summary>
    public class ClothesImageDatabaseValidateServiceTest : ClothesImageDatabaseValidateService
    {
        public ClothesImageDatabaseValidateServiceTest()
         : base(ClothesImageTable.Object,
                ClothesDatabaseValidateServiceMock.GetClothesDatabaseValidateService(ClothesEntitiesData.ClothesEntities))
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var image = ClothesImageData.ClothesImageDomains.First();

            var result = ValidateModel(image);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_ImageError()
        {
            var imageEmptyImage = new ClothesImageDomain(0, null!, false, 0);

            var result = ValidateModel(imageEmptyImage);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotValid);
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_Ok()
        {
            var imageDomain = ClothesImageData.GetClothesImageFromClothes(ClothesEntitiesData.ClothesEntities.First().Id).First();

            var result = await ValidateIncludes(imageDomain);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Одежда не найдена
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_ClothesNotFound()
        {
            var imageDomain = ClothesImageData.GetClothesImageFromClothes(0).First();

            var result = await ValidateIncludes(imageDomain);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_Ok()
        {
            var imageDomains = ClothesImageData.GetClothesImageFromClothes(ClothesEntitiesData.ClothesEntities.First().Id);

            var result = await ValidateIncludes(imageDomains);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Одежда не найдена
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_ImagesNotFound()
        {
            var imageDomains = ClothesImageData.GetClothesImageFromClothes(ClothesEntitiesData.ClothesEntities.First().Id).
                               Append(new ClothesImageDomain(0, ClothesImageData.ClothesImageDomains.First().Image, false, 0));

            var result = await ValidateIncludes(imageDomains);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить по наличию главного изображения
        /// </summary>
        [Fact]
        public void ValidateByMain_Ok()
        {
            var imageDomains = ClothesImageData.ClothesImageDomains;

            var result = ValidateByMain(imageDomains);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        private static Mock<IClothesImageTable> ClothesImageTable =>
            new();
    }
}