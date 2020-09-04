using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель. Тесты
    /// </summary>
    public class GenderDtoConverterTest
    {
        /// <summary>
        /// Преобразование в трансферную модель и обратно
        /// </summary>
        [Fact]
        public void GenderToDto_FromDto()
        {
            var gender = GetGender();

            var genderDto = GenderDtoConverter.ToDto(gender);
            var genderAfterConverter = GenderDtoConverter.FromDto(genderDto);

            Assert.True(gender.Equals(genderAfterConverter));
        }

        /// <summary>
        /// Преобразование в трансферную модель коллекцию и обратно
        /// </summary>
        [Fact]
        public void GenderToDtoCollection_FromDtoCollection()
        {
            var genders = GetGenders();

            var gendersDto = GenderDtoConverter.ToDtoCollection(genders);
            var gendersAfterConverter = GenderDtoConverter.FromDtoCollection(gendersDto);

            Assert.True(genders.SequenceEqual(gendersAfterConverter));
        }

        /// <summary>
        /// Преобразование в Json и обратно
        /// </summary>
        [Fact]
        public void GenderToJson_FromJson()
        {
            var gender = GetGender();

            var genderAfterConverter = GenderDtoConverter.ToJson(gender).
                                       ResultValueBindOk(GenderDtoConverter.FromJson);

            Assert.True(genderAfterConverter.OkStatus);
            Assert.True(gender.Equals(genderAfterConverter.Value));
        }

        /// <summary>
        /// Преобразование из некорректного Json
        /// </summary>
        [Fact]
        public void GenderFromIncorrectJson()
        {
            var genderAfterConverter = GenderDtoConverter.FromJson(String.Empty);

            Assert.True(genderAfterConverter.HasErrors);
        }

        /// <summary>
        /// Преобразование коллекции в Json и обратно
        /// </summary>
        [Fact]
        public void GendersToJson_FromJson()
        {
            var genders = GetGenders();

            var gendersAfterConverter = GenderDtoConverter.ToJsonCollection(genders).
                                        ResultValueBindOk(GenderDtoConverter.FromJsonCollection);

            Assert.True(gendersAfterConverter.OkStatus);
            Assert.True(genders.SequenceEqual(gendersAfterConverter.Value));
        }

        /// <summary>
        /// Преобразование из некорректного Json в коллекцию
        /// </summary>
        [Fact]
        public void GendersFromIncorrectJson()
        {
            var gendersAfterConverter = GenderDtoConverter.FromJsonCollection(String.Empty);

            Assert.True(gendersAfterConverter.HasErrors);
        }

        /// <summary>
        /// Тестовый пол
        /// </summary>
        private static Gender GetGender() => new Gender(GenderType.Male, "Мужик");

        /// <summary>
        /// Тестовая коллекция пола
        /// </summary>
        private static IReadOnlyCollection<Gender> GetGenders() =>
            new List<Gender>
            {
                new Gender(GenderType.Male, "Мужик"),
                new Gender(GenderType.Female, "Баба"),
            };
    }
}