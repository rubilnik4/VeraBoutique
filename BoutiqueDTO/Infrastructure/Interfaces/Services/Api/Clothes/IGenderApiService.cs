using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Routes.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис типа пола
    /// </summary>
    public interface IGenderApiService: IApiService<GenderType, GenderTransfer>
    {
        /// <summary>
        /// Получить данные типа пола с категорией
        /// </summary>
        public IResultCollection<GenderCategoryTransfer> GetGenderCategories();

        /// <summary>
        /// Получить данные типа пола с категорией асинхронно
        /// </summary>
        public Task<IResultCollection<GenderCategoryTransfer>> GetGenderCategoriesAsync();
    }
}