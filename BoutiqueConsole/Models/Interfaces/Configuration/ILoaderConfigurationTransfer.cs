﻿using System;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueConsole.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация консольного загрузчика. Трансферная модель
    /// </summary>
    public interface ILoaderConfigurationTransfer : ILoaderConfigurationBase<HostConfigurationTransfer>, ITransferModel<Guid>
    { }
}