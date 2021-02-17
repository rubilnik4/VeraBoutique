using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;

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