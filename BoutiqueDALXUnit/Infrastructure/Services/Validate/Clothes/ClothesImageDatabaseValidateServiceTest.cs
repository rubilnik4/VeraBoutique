using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
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
         : base(ClothesImageTable.Object)
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
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
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
        /// Проверить по наличию главного изображения. Отсутствие
        /// </summary>
        [Fact]
        public void ValidateByMain_Empty()
        {
            var imageDomains = ClothesImageData.ClothesImageDomains.Where(clothesImage => !clothesImage.IsMain);

            var result = ValidateByMain(imageDomains);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить по наличию главного изображения. Несколько главных
        /// </summary>
        [Fact]
        public void ValidateByMain_Multiple()
        {
            var imageDomains = ClothesImageData.ClothesImageDomains.
                               Append(new ClothesImageDomain(0, ClothesImageData.ClothesImageDomains.First().Image, true, 0));

            var result = ValidateByMain(imageDomains);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        private static Mock<IClothesImageTable> ClothesImageTable =>
            new();
    }
}