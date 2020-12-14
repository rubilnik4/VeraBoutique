using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Refit.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using Refit;

namespace BoutiqueDTO.Infrastructure.Interfaces.Refit.Clothes
{
    /// <summary>
    /// Api сервис. Тип пола
    /// </summary>
    public interface IGenderRefitApiBase: IRefitApiBase
    {
        /// <summary>
        /// Наименование роута
        /// </summary>
        public const string GENDER = nameof(GENDER);
        
        /// <summary>
        /// Получить типы пола
        /// </summary>
        [Get("/" + GENDER)]
        Task<IReadOnlyCollection<GenderTransfer>> GetGenders();
    }
}