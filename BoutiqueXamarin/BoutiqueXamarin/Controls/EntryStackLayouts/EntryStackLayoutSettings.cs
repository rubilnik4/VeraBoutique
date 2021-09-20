namespace BoutiqueXamarin.Controls.EntryStackLayouts
{
    /// <summary>
    /// Свойства элемента текстового поля
    /// </summary>
    public class EntryStackLayoutSettings
    {
        public EntryStackLayoutSettings(int passwordMinimumLength, bool passwordNeedDigit)
        {
            PasswordMinimumLength = passwordMinimumLength;
            PasswordNeedDigit = passwordNeedDigit;
        }

        /// <summary>
        /// Минимальное количество символов в пароле
        /// </summary>
        public int PasswordMinimumLength { get; }

        /// <summary>
        /// Необходимость наличия цифры в пароле
        /// </summary>
        public bool PasswordNeedDigit { get; }
    }
}