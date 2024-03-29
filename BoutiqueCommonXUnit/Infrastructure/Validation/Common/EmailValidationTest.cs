﻿using BoutiqueCommon.Infrastructure.Implementation.Validation.Common;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation.Common
{
    /// <summary>
    /// Проверка почты. Тесты
    /// </summary>
    public class EmailValidationTest
    {
        [Theory]
        [InlineData("rubilnik4@yandex.ru", true)]
        [InlineData("rubilnik4@yandex", false)]
        [InlineData("rubilnik4yandex.ru", false)]
        public void IsValid(string email, bool validation)
        {
            bool isValid = EmailValidation.IsValid(email);

            Assert.Equal(validation, isValid);
        }
    }
}