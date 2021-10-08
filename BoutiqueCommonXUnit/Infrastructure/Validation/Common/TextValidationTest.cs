using BoutiqueCommon.Infrastructure.Implementation.Validation.Common;
using Xunit;

namespace BoutiqueCommonXUnit.Infrastructure.Validation.Common
{
    /// <summary>
    /// Проверка текста. Тесты
    /// </summary>
    public class TextValidationTest
    {
        [Theory]
        [InlineData("text", true)]
        [InlineData("Text", true)]
        [InlineData("", false)]
        [InlineData("text2", false)]
        [InlineData("//text", false)]
        [InlineData("text!", false)]
        public void IsValid(string text, bool validation)
        {
            bool isValid = TextValidation.IsValid(text);

            Assert.Equal(validation, isValid);
        }
    }
}