using BoutiqueDTOXUnit.Data.Models.Interfaces;

namespace BoutiqueDTOXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая вложенная трансферная модель
    /// </summary>
    public class TestIncludeTransfer:  ITestIncludeTransfer
    {
        public TestIncludeTransfer() { }

        public TestIncludeTransfer(string name) 
        {
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = null!;
    }
}