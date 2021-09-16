using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueXamarin.Controls.Enums;

namespace BoutiqueXamarin.Controls.Extensions
{
    /// <summary>
    /// Проверка текстовых полей
    /// </summary>
    public static class EntryValidation
    {
        /// <summary>
        /// Выполнить проверку
        /// </summary>
        public static bool IsValid(EntryValidationType entryValidationType, string text) =>
            entryValidationType switch
            {
                EntryValidationType.Email => EmailValidation.IsValidEmail(text),
                _ => true,
            };
    }
}