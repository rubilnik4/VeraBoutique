using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;

namespace BoutiqueDTO.Data.Services.Interfaces
{
    /// <summary>
    /// Тестовый конвертер трансферных моделей
    /// </summary>
    public interface ITestTransferConverter: ITransferConverter<TestEnum, ITestDomain, ITestTransfer>
    { }
}