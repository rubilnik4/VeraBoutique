using System.Xml;
using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueXamarin.Controls.Enums;
using Xamarin.Forms;

namespace BoutiqueXamarin.Controls.Extensions
{
    /// <summary>
    /// Преобразование типа проверки в тип ввода
    /// </summary>
    public static class EntryValidationInputType
    {
        /// <summary>
        /// Преобразовать тип проверки в тип ввода
        /// </summary>
        public static Keyboard ToInputType(this EntryValidationType entryValidationType) =>
            entryValidationType switch
            {
                EntryValidationType.Email => Keyboard.Email,
                EntryValidationType.Phone => Keyboard.Telephone,
                EntryValidationType.Text => Keyboard.Text,
                EntryValidationType.Numeric => Keyboard.Numeric,
                _ => Keyboard.Default,
            };
    }
}