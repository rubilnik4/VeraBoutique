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
    public class SizeGroupTransfer : SizeGroupBase<SizeTransfer>, ISizeGroupTransfer
    {
        public SizeGroupTransfer(ISizeGroupShortBase sizeGroup, IEnumerable<SizeTransfer> sizes)
            : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizes.ToList())
        { }

        [JsonConstructor]
        public SizeGroupTransfer(ClothesSizeType clothesSizeType, int sizeNormalize, IReadOnlyCollection<SizeTransfer> sizes)
            : base(clothesSizeType, sizeNormalize, sizes)
        { }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id => GetIdHashCode(ClothesSizeType, SizeNormalize);
    }
}