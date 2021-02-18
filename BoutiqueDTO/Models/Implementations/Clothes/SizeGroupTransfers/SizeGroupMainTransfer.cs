using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers
{
    /// <summary>
    /// Группа размеров одежды разного типа. Трансферная модель
    /// </summary>
    public class SizeGroupMainTransfer : SizeGroupMainBase<SizeTransfer>, ISizeGroupMainTransfer
    {
        public SizeGroupMainTransfer(ISizeGroupBase sizeGroup, IEnumerable<SizeTransfer> sizes)
            : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizes.ToList())
        { }

        [JsonConstructor]
        public SizeGroupMainTransfer(ClothesSizeType clothesSizeType, int sizeNormalize, IReadOnlyCollection<SizeTransfer> sizes)
            : base(clothesSizeType, sizeNormalize, sizes)
        { }
    }
}