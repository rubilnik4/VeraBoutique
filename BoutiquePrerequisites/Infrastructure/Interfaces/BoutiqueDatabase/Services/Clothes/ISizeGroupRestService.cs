using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Base;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes
{
    /// <summary>
    /// Сервис загрузки группы размеров одежды в базу данных
    /// </summary>
    public interface ISizeGroupRestService : IRestServiceBase<(ClothesSizeType, int), ISizeGroupDomain>
    { }
}