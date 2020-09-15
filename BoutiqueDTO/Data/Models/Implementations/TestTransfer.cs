using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Data.Models.Interfaces;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая трансферная модель
    /// </summary>
    public class TestTransfer : Test, ITestTransfer
    {
        public TestTransfer(TestEnum testEnum, string name)
            : base(testEnum, name)
        { }
    }
}