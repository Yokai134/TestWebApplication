using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Interface.InterfaceRepository;
using TestWebApplication.Model;

namespace TestWebApplication.Repository
{
    /// <summary>
    /// Репозиторий для работы с продуктами, предоставляющий специализированные методы.
    /// </summary>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        /// <summary>
        /// Инициализирует новый экземпляр ProductRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ProductRepository(AsdfgContext context) : base(context) { }

        /// <summary>
        /// Получает все продукты с информацией о соответствующих категориях.
        /// </summary>
        /// <returns>Коллекция продуктов с данными категорий.</returns>
        public async Task<IEnumerable<ProductViewModel>> GetAllWithCategoryAsync()
        {
            var selectedProducts = await (from product in _context.Products
                                          join category in _context.Categories
                                           on product.CategoryId equals category.Id
                                          select new ProductViewModel
                                          {
                                              ProductId = product.Id,
                                              Productname = product.Productname,
                                              Productcount = product.Productcount,
                                              Categoryname = category.Categoryname,
                                          }).ToListAsync();

            return selectedProducts;
        }
    }
}
