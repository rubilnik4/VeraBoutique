using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис вида одежды в базе данных
    /// </summary>
    public class ClothesTypeDatabaseService : DatabaseService<string, IClothesTypeDomain, ClothesTypeEntity>, IClothesTypeDatabaseService
    {
        public ClothesTypeDatabaseService(IDatabase database,
                                          IClothesTypeTable clothesTypeTable,
                                          IGenderTable genderTable,
                                          IClothesTypeEntityConverter clothesTypeEntityConverter)
            : base(database, clothesTypeTable, clothesTypeEntityConverter)
        {
            _clothesTypeTable = clothesTypeTable;
            _genderTable = genderTable;
            _clothesTypeEntityConverter = clothesTypeEntityConverter;
        }

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        private readonly IClothesTypeTable _clothesTypeTable;

        /// <summary>
        /// Таблица базы данных типа пола
        /// </summary>
        private readonly IGenderTable _genderTable;

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private readonly IClothesTypeEntityConverter _clothesTypeEntityConverter;

        /// <summary>
        /// Получить вид одежды по типу пола
        /// </summary>
        public async Task<IResultCollection<IClothesTypeDomain>> GetByGender(GenderType genderType) =>
            await _genderTable.FindAsync(genderType, genderEntity => genderEntity.ClothesTypeGenderEntities).
            ResultValueOkToCollectionTaskAsync(gender => gender.ClothesTypeGenderEntities).
            ResultCollectionOkTaskAsync(clothesTypeGenders => clothesTypeGenders.Select(clothesTypeGender => clothesTypeGender.ClothesTypeId)).
            ResultCollectionBindOkBindAsync(FindClothesTypeByGender).
            ResultCollectionOkTaskAsync(clothesTypes => _clothesTypeEntityConverter.FromEntities(clothesTypes));

       
        /// <summary>
        /// Создать модель базы данных для удаления по идентификатору
        /// </summary>
        protected override ClothesTypeEntity CreateRemoveEntityById(string id) =>
            new ClothesTypeEntity(id);

        /// <summary>
        /// Найти виды одежды по идентификаторам
        /// </summary>
        private async Task<IResultCollection<ClothesTypeEntity>> FindClothesTypeByGender(IEnumerable<string> clothesTypeIds) =>
            await _clothesTypeTable.
            FindAsync<string, ClothesTypeGenderEntity>(clothesTypeIds, clothesTypeEntity => clothesTypeEntity.ClothesTypeGenderEntities);
    }
}