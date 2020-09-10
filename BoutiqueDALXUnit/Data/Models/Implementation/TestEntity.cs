using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDALXUnit.Data.Database.Implementation;

namespace BoutiqueDALXUnit.Data.Models.Implementation
{
    /// <summary>
    /// Тип пола для тестов
    /// </summary>
    public class TestEntity : Test, IEntityModel<TestEnum>
    {
        public TestEntity(TestEnum testEnum, string name)
            : base(testEnum, name)
        { }
    }
}