using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueXamarinCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовый класс контейнера
    /// </summary>
    public interface ITestContainer
    {
        /// <summary>
        /// Копирование типа пола
        /// </summary>
        IGenderDomain CopyGender(IGenderDomain gender, string name);
    }
}