using System.Threading.Tasks;

namespace FunctionalXUnit.Models.Mocks.Implementation
{
    /// <summary>
    /// Примеры асинхронных функций
    /// </summary>
    public static class AsyncFunctions
    {
        /// <summary>
        /// Преобразовать число в строку асинхронно
        /// </summary>
        public static async Task<string> IntToStringAsync(int number) =>
            await Task.FromResult(number.ToString());
    }
}