using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы категорий одежды
    /// </summary>
    public static class GenderDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        public static IGenderDatabaseValidateService GetGenderDatabaseValidateService(IEnumerable<GenderEntity> genders) =>
            new GenderDatabaseValidateService(GenderTableMock.GetGenderTable(genders));
    }
}