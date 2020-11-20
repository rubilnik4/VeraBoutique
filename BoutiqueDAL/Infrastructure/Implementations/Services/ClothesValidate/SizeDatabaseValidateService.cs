﻿using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы размера одежды
    /// </summary>
    public class SizeDatabaseValidateService : DatabaseValidateService<(SizeType, string), ISizeDomain, SizeEntity>,
                                               ISizeDatabaseValidateService
    {
        public SizeDatabaseValidateService(ISizeTable sizeTable)
            : base(sizeTable)
        { }
    }
}