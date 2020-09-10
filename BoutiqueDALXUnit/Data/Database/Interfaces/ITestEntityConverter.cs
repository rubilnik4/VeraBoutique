using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;

namespace BoutiqueDALXUnit.Data.Database.Interfaces
{
    /// <summary>
    /// Тестовый конвертер сущностей
    /// </summary>
    public interface ITestEntityConverter: IEntityConverter<TestEnum, ITestDomain, TestEntity>
    { }
}