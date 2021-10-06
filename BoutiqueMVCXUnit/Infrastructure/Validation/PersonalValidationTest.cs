using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueMVC.Infrastructure.Implementation.Validation;
using BoutiqueMVC.Models.Implementations.Identity;
using Xunit;

namespace BoutiqueMVCXUnit.Infrastructure.Validation
{
    /// <summary>
    /// Проверка личных данных
    /// </summary>
    public class PersonalValidationTest
    {
        /// <summary>
        /// Проверить личные данные
        /// </summary>
        [Fact]
        public void Validate_Ok()
        {
            var personal = new PersonalDomain("Name", "Surname", "Address", "+79224725777");
            var validate = PersonalValidation.PersonalValidate(personal);

            Assert.True(validate.OkStatus);
        }

        /// <summary>
        /// Проверить личные данные
        /// </summary>
        [Fact]
        public void Validate_ErrorName()
        {
            var personal = new PersonalDomain("Name1", "Surname", "Address", "+79224725777");
            var validate = PersonalValidation.PersonalValidate(personal);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Проверить личные данные
        /// </summary>
        [Fact]
        public void Validate_ErrorSurname()
        {
            var personal = new PersonalDomain("Name", "Surname1", "Address", "+79224725777");
            var validate = PersonalValidation.PersonalValidate(personal);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Проверить личные данные
        /// </summary>
        [Fact]
        public void Validate_ErrorAddress()
        {
            var personal = new PersonalDomain("Name", "Surname", "", "+79224725777");
            var validate = PersonalValidation.PersonalValidate(personal);

            Assert.True(validate.HasErrors);
        }

        /// <summary>
        /// Проверить личные данные
        /// </summary>
        [Fact]
        public void Validate_ErrorPhone()
        {
            var personal = new PersonalDomain("Name", "Surname", "Address", "+7922477");
            var validate = PersonalValidation.PersonalValidate(personal);

            Assert.True(validate.HasErrors);
        }
    }
}