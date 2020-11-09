﻿using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных размеров одежды
    /// </summary>
    public interface ISizeTable : IDatabaseTable<(SizeType, string), SizeEntity>
    { }
}