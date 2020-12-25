using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Base;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Clothes
{
    /// <summary>
    /// Сервис загрузки типа пола в базу данных
    /// </summary>
    public interface IGenderUploadService: IUploadServiceBase<GenderType, IGenderDomain>
    { }
}