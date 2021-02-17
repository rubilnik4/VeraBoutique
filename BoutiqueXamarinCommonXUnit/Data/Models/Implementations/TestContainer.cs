using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueXamarinCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueXamarinCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовый класс контейнера
    /// </summary>
    public class TestContainer: ITestContainer
    {
        /// <summary>
        /// Копирование типа пола
        /// </summary>
        public IGenderDomain CopyGender(IGenderDomain gender, string name) =>
            new GenderDomain(gender.GenderType, name);
    }
}