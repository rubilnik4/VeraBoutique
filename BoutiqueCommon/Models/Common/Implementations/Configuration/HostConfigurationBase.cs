﻿using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;

namespace BoutiqueCommon.Models.Common.Implementations.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу
    /// </summary>
    public abstract class HostConfigurationBase: IHostConfigurationBase
    {
        protected HostConfigurationBase(Uri host, TimeSpan timeOut)
        {
            Host = host;
            TimeOut = timeOut;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Host.Host;

        /// <summary>
        /// Имя сервера
        /// </summary>
        public Uri Host { get; }

        /// <summary>
        /// Время ожидания
        /// </summary>
        public TimeSpan TimeOut { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
          obj is IHostConfigurationBase configuration && Equals(configuration);

        public bool Equals(IHostConfigurationBase? other) =>
            other?.Id == Id;

        public override int GetHashCode() =>
            HashCode.Combine(Host);
        #endregion
    }
}