using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис вида одежды в базе данных
    /// </summary>
    public class ClothesTypeDatabaseService : DatabaseService<string, IClothesTypeDomain, ClothesTypeEntity>, IClothesTypeDatabaseService
    {
        public ClothesTypeDatabaseService(IDatabase database,
                                          IDatabaseTable<string, ClothesTypeEntity> clothesTypeTable,
                                          IEntityConverter<string, IClothesTypeDomain, ClothesTypeEntity> clothesTypeEntityConverter)
            : base(database, clothesTypeTable, clothesTypeEntityConverter)
        { }

        /// <summary>
        /// Создать модель базы данных для удаления по идентификатору
        /// </summary>
        protected override ClothesTypeEntity CreateRemoveEntityById(string id) =>
            new ClothesTypeEntity(id);
    }
}