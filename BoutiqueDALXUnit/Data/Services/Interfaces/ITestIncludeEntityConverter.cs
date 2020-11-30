using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDALXUnit.Data.Models.Implementation;

namespace BoutiqueDALXUnit.Data.Services.Interfaces
{
    /// <summary>
    /// Преобразования вложенной доменной модели в модель базы данных
    /// </summary>
    public interface ITestIncludeEntityConverter : IEntityConverter<string, ITestIncludeDomain, TestIncludeEntity>
    { }
}