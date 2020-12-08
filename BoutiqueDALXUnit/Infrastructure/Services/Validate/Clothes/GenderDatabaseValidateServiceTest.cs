using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using Functional.Models.Enums;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы пола одежды
    /// </summary>
    public class GenderDatabaseValidateServiceTest : GenderDatabaseValidateService
    {
        public GenderDatabaseValidateServiceTest()
           : base(GenderTable.Object)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var gender = GenderData.GendersDomain.First();

            var result = ValidateModel(gender);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var genderEmptyName = new GenderDomain(GenderType.Female, String.Empty);

            var result = ValidateModel(genderEmptyName);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotValid);
        }

        /// <summary>
        /// Таблица базы данных типа пола
        /// </summary>
        private static Mock<IGenderTable> GenderTable =>
            new Mock<IGenderTable>();
    }
}