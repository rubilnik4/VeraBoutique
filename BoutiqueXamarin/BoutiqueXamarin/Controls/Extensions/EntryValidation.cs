﻿using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueCommon.Infrastructure.Implementation.Validation.Common;
using BoutiqueXamarin.Controls.EntryStackLayouts;
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
        public static bool IsValid(EntryValidationType entryValidationType, string text, EntryStackLayoutSettings settings) =>
            entryValidationType switch
            {
                EntryValidationType.Email => EmailValidation.IsValid(text),
                EntryValidationType.Password => PasswordValidation.IsValid(text, settings.PasswordMinimumLength, settings.PasswordNeedDigit),
                EntryValidationType.Text => TextValidation.IsValid(text),
                EntryValidationType.Phone => PhoneValidation.IsValid(text),
                _ => EmptyValidation.IsValid(text),
            };
    }
}