﻿using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы группы размера одежды
    /// </summary>
    public interface ISizeGroupDatabaseValidateService : IDatabaseValidateService<int, ISizeGroupMainDomain>
    { }
}