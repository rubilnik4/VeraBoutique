﻿using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;

namespace BoutiqueDTOXUnit.Data.Services.Interfaces.Converters
{
    /// <summary>
    /// Тестовый конвертер включенных трансферных моделей
    /// </summary>
    public interface ITestIncludesTransferConverter : ITransferConverter<string, ITestIncludeDomain, TestIncludeTransfer>
    { }
}