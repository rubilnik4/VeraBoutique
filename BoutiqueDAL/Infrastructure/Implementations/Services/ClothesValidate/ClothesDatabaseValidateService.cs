using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы одежды
    /// </summary>
    public class ClothesDatabaseValidateService : DatabaseValidateService<int, IClothesDomain, ClothesEntity>,
                                                  IClothesDatabaseValidateService
    {
        public ClothesDatabaseValidateService(IClothesTable clothesTable,
                                              IGenderDatabaseValidateService genderDatabaseValidateService,
                                              IClothesTypeDatabaseValidateService clothesTypeDatabaseValidateService,
                                              IColorClothesDatabaseValidateService colorClothesDatabaseValidateService,
                                              ISizeGroupDatabaseValidateService sizeGroupDatabaseValidateService)
            : base(clothesTable)
        {
            _genderDatabaseValidateService = genderDatabaseValidateService;
            _clothesTypeDatabaseValidateService = clothesTypeDatabaseValidateService;
            _colorClothesDatabaseValidateService = colorClothesDatabaseValidateService;
            _sizeGroupDatabaseValidateService = sizeGroupDatabaseValidateService;
        }

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private readonly IGenderDatabaseValidateService _genderDatabaseValidateService;

        /// <summary>
        /// Сервис проверки данных из базы типов одежды
        /// </summary>
        private readonly IClothesTypeDatabaseValidateService _clothesTypeDatabaseValidateService;

        /// <summary>
        /// Сервис проверки данных из базы цвета одежды
        /// </summary>
        private readonly IColorClothesDatabaseValidateService _colorClothesDatabaseValidateService;

        /// <summary>
        /// Сервис проверки данных из базы группы размера одежды
        /// </summary>
        private readonly ISizeGroupDatabaseValidateService _sizeGroupDatabaseValidateService;

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        public override async Task<IResultError> ValidateIncludes(IClothesDomain clothes) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => _genderDatabaseValidateService.ValidateFind(clothes.Gender)).
            ResultErrorBindOkBindAsync(() => _clothesTypeDatabaseValidateService.ValidateFind(clothes.ClothesTypeShort)).
            ResultErrorBindOkBindAsync(() => _genderDatabaseValidateService.ValidateQuantityFind(clothesType.Genders));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        public override async Task<IResultError> ValidateIncludes(IEnumerable<IClothesTypeDomain> clothesTypes) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => clothesTypes.Select(clothesType => clothesType.Category).Distinct().
                                         Map(categories => _categoryDatabaseValidateService.ValidateQuantityFind(categories))).
            ResultErrorBindOkTaskAsync(() => clothesTypes.
                                             Select(clothesType => _genderDatabaseValidateService.ValidateQuantity(clothesType.Genders)).
                                             ToResultError()).
            ResultErrorBindOkBindAsync(() => clothesTypes.SelectMany(clothesType => clothesType.Genders).Distinct().
                                             Map(genders => _genderDatabaseValidateService.ValidateFinds(genders)));
    }
}