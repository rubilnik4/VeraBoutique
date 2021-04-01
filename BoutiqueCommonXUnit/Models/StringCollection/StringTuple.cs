namespace BoutiqueCommonXUnit.Models.StringCollection
{
    /// <summary>
    /// Кортеж строк
    /// </summary>
    public class StringTuple
    {
        public StringTuple(string? firstString, string? secondString)
        {
            FirstString = firstString;
            SecondString = secondString;
        }

        /// <summary>
        /// Первая строка
        /// </summary>
        public string? FirstString { get; }

        /// <summary>
        /// Вторая строка
        /// </summary>
        public string? SecondString { get; }
    }
}