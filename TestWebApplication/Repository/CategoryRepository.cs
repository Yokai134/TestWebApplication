using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Interface.InterfaceRepository;
using TestWebApplication.Model;

namespace TestWebApplication.Repository
{
    /// <summary>
    /// Репозиторий для работы с категориями, предоставляющий специализированные методы.
    /// </summary>
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр CategoryRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public CategoryRepository(AsdfgContext context) : base(context) { }

        /// <summary>
        /// Получает все продукты, принадлежащие указанной категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Коллекция продуктов категории.</returns>
        public async Task<IEnumerable<Product>> GetByCategoryId(int id)
        {
            var selectProduct = await _context.Products
                .Where(p => p.CategoryId == id).ToListAsync();

            return selectProduct;
        }
    }
}
