namespace TestWebApplication.Model
{
    /// <summary>
    /// Модель представления для отображения информации о товаре с данными категории.
    /// Используется для передачи данных в UI без циклических ссылок.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string Productname { get; set; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Productcount { get; set; }

        /// <summary>
        /// Наименование категории, к которой принадлежит товар.
        /// </summary>
        public string Categoryname { get; set; }
    }
}
