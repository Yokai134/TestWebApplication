namespace TestBlazorApp.Model
{
    /// <summary>
    /// Представляет категорию товаров в системе.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование категории. Обязательное поле.
        /// </summary>
        public string Categoryname { get; set; } = null!;
    }
}
