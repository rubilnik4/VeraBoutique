using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementations.Clothes;
using BoutiqueCommon.Models.Interfaces.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
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
    public class GenderService : DatabaseService<GenderType, IGender, IGenderEntity>, IGenderService
    {
        public GenderService(IResultValue<IDatabaseTable<GenderType, IGenderEntity>> genderDatabaseTable,
                             IEntityConverter<GenderType, IGender, IGenderEntity> genderEntityConverter)
            :base(genderDatabaseTable, genderEntityConverter)
        { }
    }
}