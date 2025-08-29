namespace TestBlazorApp.Model
{
    /// <summary>
    /// Представляет товар в системе.
    /// </summary>
    public class CreateProductModel
    {
        /// <summary>
        /// Уникальный идентификатор товара.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Productname { get; set; } = null!;

        /// <summary>
        /// Идентификатор категории, к которой принадлежит товар.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Productcount { get; set; }
    }
}
