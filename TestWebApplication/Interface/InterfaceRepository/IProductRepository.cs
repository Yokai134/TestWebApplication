using TestWebApplication.Model;

namespace TestWebApplication.Interface.InterfaceRepository
{
    /// <summary>
    /// Определяет операции для работы с продуктами, расширяя базовый интерфейс IRepository.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Получить все продукты с информацией о категории.
        /// </summary>
        /// <returns>Коллекция продуктов с данными категорий.</returns>
        Task<IEnumerable<ProductViewModel>> GetAllWithCategoryAsync();
    }
}
