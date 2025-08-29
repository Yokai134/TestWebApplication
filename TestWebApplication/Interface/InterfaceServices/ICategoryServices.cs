using TestWebApplication.Model;

namespace TestWebApplication.Interface.InterfaceServices
{
    /// <summary>
    /// Определяет бизнес-логику для работы с категориями.
    /// </summary>
    public interface ICategoryServices
    {
        /// <summary>Получить все категории.</summary>
        Task<IEnumerable<Category>> GetCategoriesAsync();

        /// <summary>Получить категорию по идентификатору.</summary>
        /// <param name="id">Идентификатор категории.</param>
        Task<Category> GetCategoryByIdAsync(int id);

        /// <summary>Создать новую категорию.</summary>
        /// <param name="category">Данные новой категории.</param>
        Task<Category> PostCategoryAsync(Category category);

        /// <summary>Обновить существующую категорию.</summary>
        /// <param name="category">Данные для обновления категории.</param>
        Task PutCategoryAsync(Category category);

        /// <summary>Удалить категорию по идентификатору.</summary>
        /// <param name="id">Идентификатор категории для удаления.</param>
        Task DeleteCategoryAsync(int id);

        /// <summary>Получить продукты указанной категории.</summary>
        /// <param name="id">Идентификатор категории.</param>
        Task<IEnumerable<Product>> GetProductsAsync(int id);
    }
}
