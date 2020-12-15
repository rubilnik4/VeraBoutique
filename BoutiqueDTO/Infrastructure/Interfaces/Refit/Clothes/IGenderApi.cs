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
    public interface IGenderApi: IApiBase
    {
        /// <summary>
        /// Наименование роута
        /// </summary>
        public const string GENDER = nameof(GENDER);
        
        /// <summary>
        /// Получить типы пола
        /// </summary>
        [Get("/" + GENDER)]
        Task<IReadOnlyCollection<GenderTransfer>> Get();

        /// <summary>
        /// Получить тип пола по идентификатору
        /// </summary>
        [Get("/" + GENDER + "/{id}")]
        Task<GenderTransfer> Get(GenderType id);

        /// <summary>
        /// Записать тип пола
        /// </summary>
        [Post("/" + GENDER )]
        Task<GenderType> Post(GenderTransfer transfer);

        /// <summary>
        /// Записать типы пола
        /// </summary>
        [Post("/" + GENDER)]
        Task<IReadOnlyCollection<GenderType>> PostCollection(IReadOnlyCollection<GenderTransfer> transfers);

        /// <summary>
        /// Записать тип пола
        /// </summary>
        [Put("/" + GENDER)]
        Task Put(GenderTransfer transfer);

        /// <summary>
        /// Записать тип пола
        /// </summary>
        [Delete("/" + GENDER+ "/{id}")]
        Task<GenderTransfer> Delete(GenderType id);
    }
}