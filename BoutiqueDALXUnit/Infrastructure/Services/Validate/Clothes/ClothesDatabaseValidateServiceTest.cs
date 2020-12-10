using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using Functional.Models.Enums;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы одежды
    /// </summary>
    public class ClothesDatabaseValidateServiceTest : ClothesDatabaseValidateService
    {
        public ClothesDatabaseValidateServiceTest()
            : base(ClothesTable.Object,
                   GenderDatabaseValidateServiceMock.GetGenderDatabaseValidateService(GenderEntitiesData.GenderEntities),
                   ClothesTypeDatabaseValidateServiceMock.GetClothesTypeDatabaseValidateService(ClothesTypeEntitiesData.ClothesTypeEntities),
                   ColorClothesDatabaseValidateServiceMock.GetColorClothesDatabaseValidateService(ColorEntityData.ColorEntities),
                   SizeGroupDatabaseValidateServiceMock.GetSizeGroupDatabaseValidateService(SizeGroupEntitiesData.SizeGroupEntities))
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var clothes = ClothesData.ClothesDomains.First();

            var result = ValidateModel(clothes);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель. Ошибка имени
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var clothes = ClothesData.ClothesDomains.First();
            var clothesEmptyName = new ClothesDomain(clothes.Id, String.Empty, clothes.Description, clothes.Price, clothes.Image,
                                                     clothes.Gender, clothes.ClothesTypeShort, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyName);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotValid);
        }

        /// <summary>
        /// Проверить модель. Ошибка описания
        /// </summary>
        [Fact]
        public void ValidateModel_DescriptionError()
        {
            var clothes = ClothesData.ClothesDomains.First();
            var clothesEmptyDescription = new ClothesDomain(clothes.Id, clothes.Name, String.Empty, clothes.Price, clothes.Image,
                                                     clothes.Gender, clothes.ClothesTypeShort, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyDescription);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotValid);
        }

        /// <summary>
        /// Проверить модель. Ошибка цены
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ValidateModel_PriceError(decimal price)
        {
            var clothes = ClothesData.ClothesDomains.First();
            var clothesPrice = new ClothesDomain(clothes.Id, clothes.Name, clothes.Description, price, clothes.Image,
                                                 clothes.Gender, clothes.ClothesTypeShort, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesPrice);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotValid);
        }

        /// <summary>
        /// Проверить модель. Ошибка цветов
        /// </summary>
        [Fact]
        public void ValidateModel_ColorsError()
        {
            var clothes = ClothesData.ClothesDomains.First();
            var clothesEmptyColors = new ClothesDomain(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                                                 clothes.Gender, clothes.ClothesTypeShort, Enumerable.Empty<IColorDomain>(),
                                                 clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyColors);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.CollectionEmpty);
        }

        /// <summary>
        /// Проверить модель. Ошибка группы размеров
        /// </summary>
        [Fact]
        public void ValidateModel_SizeGroupsError()
        {
            var clothes = ClothesData.ClothesDomains.First();
            var clothesEmptySizeGroups = new ClothesDomain(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                                                 clothes.Gender, clothes.ClothesTypeShort, clothes.Colors,
                                                 Enumerable.Empty<ISizeGroupDomain>());

            var result = ValidateModel(clothesEmptySizeGroups);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.CollectionEmpty);
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_Ok()
        {
            var clothes = ClothesData.ClothesDomains.First();

            var result = await ValidateIncludes(clothes);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Тип пола не найден
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_GenderNotFound()
        {
            var gender = new GenderDomain(GenderType.Child, "NotFound");
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = new ClothesDomain(clothes, gender, clothes.ClothesTypeShort, clothes.Colors, clothes.SizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Тип одежды не найден
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_ClothesTypeNotFound()
        {
            var clothesType = new ClothesTypeShortDomain("NotFound", "NotFound");
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = new ClothesDomain(clothes, clothes.Gender, clothesType, clothes.Colors, clothes.SizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Цвета одежды не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_ColorsNotFound()
        {
            var colors = ColorData.ColorDomain.Append(new ColorDomain("NotFound"));
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = new ClothesDomain(clothes, clothes.Gender, clothes.ClothesTypeShort, colors, clothes.SizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Группы размеров не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_SizeGroupsNotFound()
        {
            var sizeGroups = SizeGroupData.SizeGroupDomains.Append(new SizeGroupDomain(ClothesSizeType.Dress,0, Enumerable.Empty<ISizeDomain>()));
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = new ClothesDomain(clothes, clothes.Gender, clothes.ClothesTypeShort, clothes.Colors, sizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_Ok()
        {
            var clothes = ClothesData.ClothesDomains;

            var result = await ValidateIncludes(clothes);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Тип пола не найден
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_GenderNotFound()
        {
            var gender = new GenderDomain(GenderType.Child, "NotFound");
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = ClothesData.ClothesDomains.
                                  Append(new ClothesDomain(clothes, gender, clothes.ClothesTypeShort, clothes.Colors, clothes.SizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Тип одежды не найден
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_ClothesTypeNotFound()
        {
            var clothesType = new ClothesTypeShortDomain("NotFound", "NotFound");
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = ClothesData.ClothesDomains.
                                  Append(new ClothesDomain(clothes, clothes.Gender, clothesType, clothes.Colors, clothes.SizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Цвета одежды не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_ColorsNotFound()
        {
            var colors = ColorData.ColorDomain.Append(new ColorDomain("NotFound"));
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = ClothesData.ClothesDomains.
                                  Append(new ClothesDomain(clothes, clothes.Gender, clothes.ClothesTypeShort, colors, clothes.SizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Группы размеров не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_SizeGroupsNotFound()
        {
            var sizeGroups = SizeGroupData.SizeGroupDomains.Append(new SizeGroupDomain(ClothesSizeType.Dress, 0, Enumerable.Empty<ISizeDomain>()));
            var clothes = ClothesData.ClothesDomains.First();
            var clothesNotFound = ClothesData.ClothesDomains.
                                  Append(new ClothesDomain(clothes, clothes.Gender, clothes.ClothesTypeShort, clothes.Colors, sizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private static Mock<IClothesTable> ClothesTable =>
            new Mock<IClothesTable>();
    }
}