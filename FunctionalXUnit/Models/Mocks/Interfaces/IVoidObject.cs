
namespace FunctionalXUnit.Models.Mocks.Interfaces
{
    /// <summary>
    /// Тестовый класс для проверки действий
    /// </summary>
    public interface IVoidObject
    {
        /// <summary>
        /// Тестовый метод
        /// </summary>
        void TestVoid();

        /// <summary>
        /// Тестовый метод с числовым параметром
        /// </summary>
        void TestNumberVoid(int number);
    }
}