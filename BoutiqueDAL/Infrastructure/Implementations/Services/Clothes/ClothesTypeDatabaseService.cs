﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using Microsoft.EntityFrameworkCore;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис вида одежды в базе данных
    /// </summary>
    public class ClothesTypeDatabaseService : DatabaseService<string, IClothesTypeMainDomain, ClothesTypeEntity>,
                                              IClothesTypeDatabaseService
    {
        public ClothesTypeDatabaseService(IBoutiqueDatabase boutiqueDatabase,
                                          IClothesTypeDatabaseValidateService clothesTypeDatabaseValidateService,
                                          IClothesTypeMainEntityConverter clothesTypeEntityConverter)
            : base(boutiqueDatabase, boutiqueDatabase.ClotheTypeTable, clothesTypeDatabaseValidateService, clothesTypeEntityConverter)
        { }
    }
}