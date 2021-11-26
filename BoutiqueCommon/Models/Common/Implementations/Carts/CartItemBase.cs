using BoutiqueCommon.Models.Common.Interfaces.Carts;

namespace BoutiqueCommon.Models.Common.Implementations.Carts
{
    /// <summary>
    /// Позиция в корзине
    /// </summary>
    public abstract class CartItemBase: ICartItemBase
    {
        protected CartItemBase(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; }
    }
}