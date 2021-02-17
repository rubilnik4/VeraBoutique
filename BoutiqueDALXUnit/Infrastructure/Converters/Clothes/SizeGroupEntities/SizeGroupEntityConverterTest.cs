using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Преобразования модели группы размера одежды в модель базы данных. Тесты
    /// </summary>
    public class SizeGroupEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity()
        {
            var sizeGroupDomain = SizeGroupData.SizeGroupDomains.First();
            var sizeGroupEntityConverter = SizeGroupEntityConverter;

            var sizeGroupEntity = sizeGroupEntityConverter.ToEntity(sizeGroupDomain);
            // var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupEntity);

            //Assert.True(sizeGroupAfterConverter.OkStatus);
            //Assert.True(sizeGroupDomain.Equals(sizeGroupAfterConverter.Value));

            Assert.True(sizeGroupDomain.Equals(sizeGroupEntity));
            Assert.True(sizeGroupEntity.SizeGroupComposites?.All(composite => composite.Size == null));
        }

        /// <summary>
        /// Преобразования модели группы размера одежды из модели базы данных
        /// </summary>
        [Fact]
        public void FromEntity()
        {
            var sizeGroupEntity = SizeGroupEntitiesData.SizeGroupEntities.First();
            var sizeGroupEntityConverter = SizeGroupEntityConverter;

            var sizeGroupDomain = sizeGroupEntityConverter.FromEntity(sizeGroupEntity);
           
            Assert.True(sizeGroupDomain.OkStatus);
            Assert.True(SizeGroupData.SizeGroupDomains.First().Equals(sizeGroupDomain.Value));
        }

        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных. Ошибка размеров одежды
        /// </summary>
        [Fact]
        public void FromEntity_SizesNotFound()
        {
            var sizeGroup = SizeGroupEntitiesData.SizeGroupEntities.First();
            var sizeGroupNull = new SizeGroupEntity(sizeGroup);
            var sizeEntityConverter = new SizeEntityConverter();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter(sizeEntityConverter);

            var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupNull);

            Assert.True(sizeGroupAfterConverter.HasErrors);
            Assert.True(sizeGroupAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private static ISizeGroupEntityConverter SizeGroupEntityConverter =>
            SizeGroupEntityConverterMock.SizeGroupEntityConverter;
    }
}