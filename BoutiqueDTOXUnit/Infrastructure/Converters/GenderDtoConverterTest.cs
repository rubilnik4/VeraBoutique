using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель. Тесты
    /// </summary>
    public class GenderDtoConverterTest
    {
        [Fact]
        public void GenderToDto_FromDto()
        {
            var gender = new Gender(GenderType.Male, "Мужик");

            var genderDto = GenderDtoConverter.ToDto(gender);
            var genderAfterConverter = GenderDtoConverter.FromDto(genderDto);

            Assert.True(gender.Equals(genderAfterConverter));
        }
    }
}