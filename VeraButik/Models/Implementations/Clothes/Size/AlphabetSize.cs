namespace VeraButik.Models.Implementations.Clothes.Size
{
    /// <summary>
    /// Размер одежды в буквенном виде
    /// </summary>
    public class AlphabetSize
    {
        public AlphabetSize(string size)
        {
            Size = size;
        }

        /// <summary>
        /// Размер
        /// </summary>
        public string Size{ get; }
    }
}