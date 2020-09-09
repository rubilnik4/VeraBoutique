using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис типа пола одежды в базе данных
    /// </summary>
    public class GenderService : DatabaseService<GenderType, IGenderDomain, GenderEntity>, IGenderService
    {
        public GenderService(IResultValue<IDatabase> database,
                             IResultValue<IDatabaseTable<GenderType, GenderEntity>> genderDatabaseTable,
                             IEntityConverter<GenderType, IGenderDomain, GenderEntity> genderEntityConverter)
            : base(database, genderDatabaseTable, genderEntityConverter)
        { }

        /// <summary>
        /// Создать модель базы данных для удаления по идентификатору
        /// </summary>
        protected override GenderEntity CreateRemoveEntityById(GenderType id) =>
            new GenderEntity(id, String.Empty);
    }
}