using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.CommonErrors;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы цвета одежды. Тесты
    /// </summary>
    public class ColorClothesDatabaseValidateServiceTest : ColorClothesDatabaseValidateService
    {
        public ColorClothesDatabaseValidateServiceTest()
            : base(ColorClothesTable.Object)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var color = ColorData.ColorDomains.First();

            var result = ValidateModel(color);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var colorEmptyName = new ColorDomain(String.Empty);

            var result = ValidateModel(colorEmptyName);

            Assert.True(result.HasErrors);
            Assert.IsType<IValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        private static Mock<IColorTable> ColorClothesTable =>
            new();
    }
}