using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;

namespace BoutiqueDTOXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая трансферная модель основных данных
    /// </summary>
    public class TestShortTransfer : ITestShortTransfer
    {
        public TestShortTransfer()
        { }

        public TestShortTransfer(TestEnum testEnum, string name)
        {
            TestEnum = testEnum;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public TestEnum Id => TestEnum;

        /// <summary>
        /// Тестовое перечисление
        /// </summary>
        public TestEnum TestEnum { get; set; }

        /// <summary>
        /// Тестовое поле
        /// </summary>
        public string Name { get; set; } = null!;
    }
}