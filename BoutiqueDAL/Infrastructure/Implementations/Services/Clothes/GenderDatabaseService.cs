using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис типа пола одежды в базе данных
    /// </summary>
    public class GenderDatabaseService : DatabaseService<GenderType, IGenderDomain, GenderEntity>, 
                                         IGenderDatabaseService
    {
        public GenderDatabaseService(IBoutiqueDatabase boutiqueDatabase,
                                     IGenderDatabaseValidateService genderDatabaseValidateService,
                                     IGenderEntityConverter genderEntityConverter,
                                     IGenderCategoryEntityConverter genderCategoryEntityConverter)
            : base(boutiqueDatabase, boutiqueDatabase.GendersTable, genderDatabaseValidateService, genderEntityConverter)
        {
            _boutiqueDatabase = boutiqueDatabase;
            _genderCategoryEntityConverter = genderCategoryEntityConverter;
        }

        /// <summary>
        /// База данных магазина
        /// </summary>
        private readonly IBoutiqueDatabase _boutiqueDatabase;

        /// <summary>
        /// Преобразования модели типа пола с категорией в модель базы данных
        /// </summary>
        private readonly IGenderCategoryEntityConverter _genderCategoryEntityConverter;

        /// <summary>
        /// Получить типы пола одежды с категорией
        /// </summary>
        public async Task<IResultCollection<IGenderCategoryDomain>> GetGenderCategories() =>
            await _boutiqueDatabase.GendersTable.
            FindsExpressionAsync(genders => genders.Include(gender => gender.GenderCategoryComposites).
                                                    ThenInclude(composite => composite.Category).
                                                    ThenInclude(category => category!.ClothesTypes)).
            ResultCollectionBindOkTaskAsync(entities => _genderCategoryEntityConverter.FromEntities(entities));
    }
}