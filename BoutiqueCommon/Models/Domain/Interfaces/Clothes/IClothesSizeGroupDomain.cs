using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public interface IClothesSizeGroupDomain : ISizeGroup, IDomainModel<string>
    { }
}