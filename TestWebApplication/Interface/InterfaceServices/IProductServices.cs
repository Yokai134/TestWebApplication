using TestWebApplication.Model;

namespace TestWebApplication.Interface.InterfaceServices
{
    /// <summary>
    /// Определяет бизнес-логику для работы с продуктами.
    /// </summary>
    public interface IProductServices
    {
        /// <summary>Получить все продукты.</summary>
        Task<IEnumerable<Product>> GetProductsAsync();

        /// <summary>Получить продукт по идентификатору.</summary>
        /// <param name="id">Идентификатор продукта.</param>
        Task<Product> GetProductById(int id);

        /// <summary>Создать новый продукт.</summary>
        /// <param name="product">Данные нового продукта.</param>
        Task<Product> PostProductAsync(Product product);

        /// <summary>Обновить существующий продукт.</summary>
        /// <param name="product">Данные для обновления продукта.</param>
        Task PutProductAsync(Product product);

        /// <summary>Удалить продукт по идентификатору.</summary>
        /// <param name="id">Идентификатор продукта для удаления.</param>
        Task DeleteProductAsync(int id);

        /// <summary>Получить продукты с информацией о категориях (ViewModel).</summary>
        Task<IEnumerable<ProductViewModel>> GetProductsTransferAsync();
    }
}
