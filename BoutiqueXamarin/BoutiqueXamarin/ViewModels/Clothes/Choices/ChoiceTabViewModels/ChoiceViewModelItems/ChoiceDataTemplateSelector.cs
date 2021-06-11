using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels.ChoiceViewModelItems
{
    /// <summary>
    /// Выбор модели для категорий одежды
    /// </summary>
    public class ChoiceDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Шаблон категорий
        /// </summary>
        public DataTemplate? CategoryTemplate { get; set; }

        /// <summary>
        /// Шаблон типа одежды
        /// </summary>
        public DataTemplate? ClothesTypeTemplate { get; set; }

        /// <summary>
        /// Выбор шаблона
        /// </summary>
        protected override DataTemplate? OnSelectTemplate(object item, BindableObject container) =>
            item switch
            {
                ChoiceCategoryViewModelItem _  => CategoryTemplate,
                ChoiceClothesTypeViewModelItem _  => ClothesTypeTemplate,
                _ => ClothesTypeTemplate,
            };
    }
}