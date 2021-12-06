using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
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
                   SizeGroupDatabaseValidateServiceMock.GetSizeGroupDatabaseValidateService(SizeGroupEntitiesData.SizeGroupEntities),
                   ClothesImageDatabaseValidateServiceMock.GetClothesImageDatabaseValidateService(ClothesImageEntitiesData.ClothesImageEntities))
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var clothes = ClothesData.ClothesMainDomains.First();

            var result = ValidateModel(clothes);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель. Ошибка имени
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesEmptyName = new ClothesMainDomain(clothes.Id, String.Empty, clothes.Description, clothes.Price, clothes.Images,
                                                        clothes.Gender, clothes.ClothesType, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyName);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить модель. Ошибка описания
        /// </summary>
        [Fact]
        public void ValidateModel_DescriptionError()
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesEmptyDescription = new ClothesMainDomain(clothes.Id, clothes.Name, String.Empty, clothes.Price, clothes.Images,
                                                     clothes.Gender, clothes.ClothesType, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyDescription);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить модель. Ошибка цены
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ValidateModel_PriceError(decimal price)
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesPrice = new ClothesMainDomain(clothes.Id, clothes.Name, clothes.Description, price, clothes.Images,
                                                 clothes.Gender, clothes.ClothesType, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesPrice);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }
        
        /// <summary>
        /// Проверить модель. Ошибка изображений
        /// </summary>
        [Fact]
        public void ValidateModel_ImagesQuantityError()
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesEmptyImages = new ClothesMainDomain(clothes.Id, clothes.Name, clothes.Description, clothes.Price, 
                                                           Enumerable.Empty<IClothesImageDomain>(),
                                                           clothes.Gender, clothes.ClothesType, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyImages);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить модель. Ошибка изображений
        /// </summary>
        [Fact]
        public void ValidateModel_ImagesHasMainError()
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesImage = ClothesImageData.ClothesImageDomains.First();
            var clothesEmptyImages = new ClothesMainDomain(clothes.Id, clothes.Name, clothes.Description, clothes.Price,
                                                           clothes.Images.Append(new ClothesImageDomain(Guid.Empty, clothesImage.Image, true, clothes.Id)),
                                                           clothes.Gender, clothes.ClothesType, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyImages);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить модель. Ошибка изображений
        /// </summary>
        [Fact]
        public void ValidateModel_ImagesModelError()
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesEmptyImages = new ClothesMainDomain(clothes.Id, clothes.Name, clothes.Description, clothes.Price,
                                                           clothes.Images.Append(new ClothesImageDomain(Guid.Empty, null!, false, clothes.Id)),
                                                           clothes.Gender, clothes.ClothesType, clothes.Colors, clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyImages);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить модель. Ошибка цветов
        /// </summary>
        [Fact]
        public void ValidateModel_ColorsError()
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesEmptyColors = new ClothesMainDomain(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Images,
                                                 clothes.Gender, clothes.ClothesType, Enumerable.Empty<IColorDomain>(),
                                                 clothes.SizeGroups);

            var result = ValidateModel(clothesEmptyColors);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить модель. Ошибка группы размеров
        /// </summary>
        [Fact]
        public void ValidateModel_SizeGroupsError()
        {
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesEmptySizeGroups = new ClothesMainDomain(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Images,
                                                 clothes.Gender, clothes.ClothesType, clothes.Colors,
                                                 Enumerable.Empty<ISizeGroupMainDomain>());

            var result = ValidateModel(clothesEmptySizeGroups);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_Ok()
        {
            var clothes = ClothesData.ClothesMainDomains.First();

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
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = new ClothesMainDomain(clothes, clothes.Images, gender, clothes.ClothesType,
                                                        clothes.Colors, clothes.SizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели. Тип одежды не найден
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_ClothesTypeNotFound()
        {
            var clothesType = new ClothesTypeDomain("NotFound", SizeType.Default, "NotFound");
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = new ClothesMainDomain(clothes, clothes.Images, clothes.Gender, clothesType, 
                                                        clothes.Colors, clothes.SizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели. Цвета одежды не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_ColorsNotFound()
        {
            var colors = ColorData.ColorDomains.Append(new ColorDomain("NotFound"));
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = new ClothesMainDomain(clothes, clothes.Images, clothes.Gender, clothes.ClothesType,
                                                        colors, clothes.SizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели. Группы размеров не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_SizeGroupsNotFound()
        {
            var sizeGroups = SizeGroupData.SizeGroupMainDomains.Append(new SizeGroupMainDomain(ClothesSizeType.Dress, 0, Enumerable.Empty<ISizeDomain>()));
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = new ClothesMainDomain(clothes, clothes.Images, clothes.Gender, clothes.ClothesType,
                                                        clothes.Colors, sizeGroups);

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_Ok()
        {
            var clothes = ClothesData.ClothesMainDomains.OrderByDescending(clothesDomain => clothesDomain.ClothesType.CategoryName);

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
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = ClothesData.ClothesMainDomains.
                                  Append(new ClothesMainDomain(clothes, clothes.Images, gender, clothes.ClothesType, 
                                                               clothes.Colors, clothes.SizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели. Тип одежды не найден
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_ClothesTypeNotFound()
        {
            var clothesType = new ClothesTypeDomain("NotFound", SizeType.Default, "NotFound");
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = ClothesData.ClothesMainDomains.
                                  Append(new ClothesMainDomain(clothes, clothes.Images, clothes.Gender, clothesType, 
                                                               clothes.Colors, clothes.SizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели. Цвета одежды не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_ColorsNotFound()
        {
            var colors = ColorData.ColorDomains.Append(new ColorDomain("NotFound"));
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = ClothesData.ClothesMainDomains.
                                  Append(new ClothesMainDomain(clothes, clothes.Images, clothes.Gender, clothes.ClothesType, 
                                                               colors, clothes.SizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели. Группы размеров не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_SizeGroupsNotFound()
        {
            var sizeGroups = SizeGroupData.SizeGroupMainDomains.Append(new SizeGroupMainDomain(ClothesSizeType.Dress, 0, Enumerable.Empty<ISizeDomain>()));
            var clothes = ClothesData.ClothesMainDomains.First();
            var clothesNotFound = ClothesData.ClothesMainDomains.
                                  Append(new ClothesMainDomain(clothes, clothes.Images, clothes.Gender, clothes.ClothesType, 
                                                               clothes.Colors, sizeGroups));

            var result = await ValidateIncludes(clothesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private static Mock<IClothesTable> ClothesTable =>
            new();
    }
}