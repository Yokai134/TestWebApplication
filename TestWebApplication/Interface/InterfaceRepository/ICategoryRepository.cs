using TestWebApplication.Model;

namespace TestWebApplication.Interface.InterfaceRepository
{
    /// <summary>
    /// Определяет операции для работы с категориями, расширяя базовый интерфейс IRepository.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Получить все продукты, принадлежащие указанной категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Коллекция продуктов категории.</returns>
        Task<IEnumerable<Product>> GetByCategoryId(int id);
    }
}
