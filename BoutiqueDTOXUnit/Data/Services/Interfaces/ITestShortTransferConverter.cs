using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;

namespace BoutiqueDTOXUnit.Data.Services.Interfaces
{
    /// <summary>
    /// Тестовый конвертер основных данных трансферных моделей
    /// </summary>
    public interface ITestShortTransferConverter : ITransferConverter<TestEnum, ITestShortDomain, TestShortTransfer>
    { }
}