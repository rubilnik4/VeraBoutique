using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
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
            var genderTransferConverter = new GenderTransferConverter();

            var genderDto = genderTransferConverter.ToTransfer(gender);
            var genderAfterConverter = genderTransferConverter.FromTransfer(genderDto);

            Assert.True(gender.Equals(genderAfterConverter));
        }

        /// <summary>
        /// Преобразование в трансферную модель коллекцию и обратно
        /// </summary>
        [Fact]
        public void GenderToDtoCollection_FromDtoCollection()
        {
            var genders = GetGenders();
            var genderTransferConverter = (IGenderTransferConverter)new GenderTransferConverter();

            var gendersDto = genderTransferConverter.ToTransfers(genders);
            var gendersAfterConverter = genderTransferConverter.FromTransfers(gendersDto);

            Assert.True(genders.SequenceEqual(gendersAfterConverter));
        }

        /// <summary>
        /// Преобразование в Json и обратно
        /// </summary>
        [Fact]
        public void GenderToJson_FromJson()
        {
            var gender = GetGender();
            var genderTransferConverter = new GenderTransferConverter();

            var genderAfterConverter = genderTransferConverter.ToJson(gender).
                                       ResultValueBindOk(genderTransferConverter.FromJson);

            Assert.True(genderAfterConverter.OkStatus);
            Assert.True(gender.Equals(genderAfterConverter.Value));
        }

        /// <summary>
        /// Преобразование из некорректного Json
        /// </summary>
        [Fact]
        public void GenderFromIncorrectJson()
        {
            var genderTransferConverter = new GenderTransferConverter();
            var genderAfterConverter = genderTransferConverter.FromJson(String.Empty);

            Assert.True(genderAfterConverter.HasErrors);
        }

        /// <summary>
        /// Преобразование коллекции в Json и обратно
        /// </summary>
        [Fact]
        public void GendersToJson_FromJson()
        {
            var genders = GetGenders();
            var genderTransferConverter = new GenderTransferConverter();

            var gendersAfterConverter = genderTransferConverter.ToJsonCollection(genders).
                                        ResultValueBindOk(genderTransferConverter.FromJsonCollection);

            Assert.True(gendersAfterConverter.OkStatus);
            Assert.True(genders.SequenceEqual(gendersAfterConverter.Value));
        }

        /// <summary>
        /// Преобразование из некорректного Json в коллекцию
        /// </summary>
        [Fact]
        public void GendersFromIncorrectJson()
        {
            var genderTransferConverter = new GenderTransferConverter();
            var gendersAfterConverter = genderTransferConverter.FromJsonCollection(String.Empty);

            Assert.True(gendersAfterConverter.HasErrors);
        }

        /// <summary>
        /// Тестовый пол
        /// </summary>
        private static IGenderDomain GetGender() => new GenderDomain(GenderType.Male, "Мужик");

        /// <summary>
        /// Тестовая коллекция пола
        /// </summary>
        private static IReadOnlyCollection<IGenderDomain> GetGenders() =>
            new List<IGenderDomain>
            {
                new GenderDomain(GenderType.Male, "Мужик"),
                new GenderDomain(GenderType.Female, "Баба"),
            };
    }
}