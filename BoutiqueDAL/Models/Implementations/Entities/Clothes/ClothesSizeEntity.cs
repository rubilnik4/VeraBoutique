using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Размер одежды. Сущность базы данных
    /// </summary>
    public class ClothesSizeEntity : Size, IClothesSizeEntity
    {
        public ClothesSizeEntity(SizeType clothesSizeType, int size, string sizeName)
            :base(clothesSizeType, size, sizeName)
        {

        }
    }
}