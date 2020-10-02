﻿using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис категорий одежды в базе данных
    /// </summary>
    public class CategoryDatabaseService : DatabaseService<string, ICategoryDomain, CategoryEntity>, ICategoryDatabaseService
    {
        public CategoryDatabaseService(IDatabase database,
                                       ICategoryTable categoryTable,
                                       ICategoryEntityConverter categoryEntityConverter)
            : base(database, categoryTable, categoryEntityConverter)
        { }
    }
}