﻿using System;
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
    /// Сервис типа пола одежды в базе данных
    /// </summary>
    public class GenderDatabaseService : DatabaseService<GenderType, IGenderDomain, GenderEntity>, IGenderDatabaseService
    {
        public GenderDatabaseService(IDatabase database,
                             IDatabaseTable<GenderType, GenderEntity> genderTable,
                             IEntityConverter<GenderType, IGenderDomain, GenderEntity> genderEntityConverter)
            : base(database, genderTable, genderEntityConverter)
        { }

        /// <summary>
        /// Создать модель базы данных для удаления по идентификатору
        /// </summary>
        protected override GenderEntity CreateRemoveEntityById(GenderType id) =>
            new GenderEntity(id, String.Empty);
    }
}