using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы размера одежды. Тесты
    /// </summary>
    public class SizeDatabaseValidateServiceTest : SizeDatabaseValidateService
    {
        public SizeDatabaseValidateServiceTest()
           : base(SizeTable.Object)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var size = SizeData.SizeDomains.First();

            var result = ValidateModel(size);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var sizeEmptyName = new SizeDomain(SizeType.American, String.Empty);

            var result = ValidateModel(sizeEmptyName);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        private static Mock<ISizeTable> SizeTable =>
            new();
    }
}