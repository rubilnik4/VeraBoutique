using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDALXUnit.Data.Entities;
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
        public void ToEntity_FromEntity()
        {
            var sizeGroupDomain = SizeGroupData.SizeGroupDomain.First();
            var sizeEntityConverter = new SizeEntityConverter();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter(sizeEntityConverter);

            var sizeGroupEntity = sizeGroupEntityConverter.ToEntity(sizeGroupDomain);
            var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupEntity);

            Assert.True(sizeGroupAfterConverter.OkStatus);
            Assert.True(sizeGroupDomain.Equals(sizeGroupAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных. Ошибка размеров одежды
        /// </summary>
        [Fact]
        public void FromEntity_SizesNotFound()
        {
            var sizeGroup = SizeGroupEntitiesData.SizeGroupEntities.First();
            var sizeGroupNull = new SizeGroupEntity(sizeGroup, null!);
            var sizeEntityConverter = new SizeEntityConverter();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter(sizeEntityConverter);

            var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupNull);

            Assert.True(sizeGroupAfterConverter.HasErrors);
            Assert.True(sizeGroupAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}