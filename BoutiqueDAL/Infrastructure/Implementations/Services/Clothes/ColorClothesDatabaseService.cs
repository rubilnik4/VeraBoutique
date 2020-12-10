﻿using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис цвета одежды в базе данных
    /// </summary>
    public class ColorClothesDatabaseService : DatabaseService<string, IColorDomain, ColorEntity>,
                                               IColorClothesDatabaseService
    {
        public ColorClothesDatabaseService(IBoutiqueDatabase boutiqueDatabase,
                                           IColorClothesDatabaseValidateService colorClothesDatabaseValidateService,
                                           IColorClothesEntityConverter colorClothesEntityConverter)
            : base(boutiqueDatabase, boutiqueDatabase.ColorClothesTable, colorClothesDatabaseValidateService, colorClothesEntityConverter)
        { }
    }
}