using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDALXUnit.Data.Models.Implementation;

namespace BoutiqueDALXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая сущность базы данных
    /// </summary>
    public interface ITestEntity: ITest, IEntityModel<TestEnum>
    {
        /// <summary>
        /// Тестовые связующие сущности
        /// </summary>
        IReadOnlyCollection<TestIncludeEntity> TestIncludeEntities { get; set; }
    }
}