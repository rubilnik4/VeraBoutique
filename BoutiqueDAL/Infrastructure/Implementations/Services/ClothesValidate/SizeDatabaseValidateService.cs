using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

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