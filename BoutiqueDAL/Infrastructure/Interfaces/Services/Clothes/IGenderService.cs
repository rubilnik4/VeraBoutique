using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementations.Clothes;
using BoutiqueCommon.Models.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис типа пола одежды в базе данных
    /// </summary>
    public interface IGenderService: IDatabaseService<GenderType, IGender>
    { }
}