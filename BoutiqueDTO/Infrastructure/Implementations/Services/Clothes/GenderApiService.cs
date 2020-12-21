using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Connection;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Api сервис типа пола
    /// </summary>
    public class GenderApiService : ApiServiceBase<GenderType, GenderTransfer>, IGenderApiService
    {
        public GenderApiService(IHostConnection hostConnection)
            : base(hostConnection)
        { }
    }
}