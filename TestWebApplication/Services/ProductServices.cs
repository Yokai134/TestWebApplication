using Microsoft.EntityFrameworkCore;
using TestWebApplication.Interface.InterfaceRepository;
using TestWebApplication.Interface.InterfaceServices;
using TestWebApplication.Model;
using TestWebApplication.Repository;

namespace TestWebApplication.Services
{
    /// <summary>
    /// Сервис для работы с бизнес-логикой продуктов.
    /// </summary>
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Инициализирует новый экземпляр ProductServices.
        /// </summary>
        /// <param name="productRepository">Репозиторий продуктов.</param>
        public ProductServices(IProductRepository productRepository) { _productRepository = productRepository; }
        
        /// <summary>
        /// Удаляет продукт идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="DbUpdateConcurrencyException">Выбрасывается, если возник конфликт параллельного доступа при удалении.</exception>
        /// <exception cref="Exception">Выбрасывается, если не удалось разрешить конфликт параллельного доступа.</exception>
        public async Task DeleteProductAsync(int id)
        {
            var deleteProduct = await _productRepository.GetAsyncById(id);
            if (deleteProduct == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            try
            {
                await _productRepository.DeleteAsync(deleteProduct);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var resourceStillExists = await _productRepository.GetAsyncById(id) != null;

                if (!resourceStillExists)
                {
                    return;
                }

                throw new Exception("Не удалось удалить ресурс из-за конфликта версий. Попробуйте еще раз.", ex);
            }
        }

        /// <summary>
        /// Получает продукт по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns>Возвращает продукт.</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывает если продукт не найден.</exception>
        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetAsyncById(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            return product;
        }

        /// <summary>
        /// Получаем список прдуктов.
        /// </summary>
        /// <returns>Отсортированниый список с продуктами.</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывает если продукты не найдены.</exception>
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var product = await _productRepository.GetAllAsync();
            if (product == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            return product.OrderBy(x=>x.Id).ToList();
        }

        /// <summary>
        /// Получаем список продуктов с иформацией по категории.
        /// </summary>
        /// <returns>Отсортированниый список продуктов с категориями.</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывает если продукты не найдены.</exception>
        public async Task<IEnumerable<ProductViewModel>> GetProductsTransferAsync()
        {
            var products = await _productRepository.GetAllWithCategoryAsync();
            if (products == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            return products.OrderBy(x => x.ProductId).ToList();
        }

        /// <summary>
        /// Добавляем новый продукт.
        /// </summary>
        /// <param name="product">Данные продукта.</param>
        /// <returns>Добавленный продукт с присвоенным идентификатором.</returns>
        public async Task<Product> PostProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            return product;
        }

        /// <summary>
        /// Обновляет данные продукта.
        /// </summary>
        /// <param name="product">Измененные данные продукта</param>
        /// <exception cref="KeyNotFoundException">Выбрасывает если продукт не найден.</exception>
        public async Task PutProductAsync(Product product)
        {
            var updateProduct = await _productRepository.GetAsyncById(product.Id);
            if(updateProduct == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            updateProduct.Productname = product.Productname;
            updateProduct.Productcount = product.Productcount;
            updateProduct.CategoryId = product.CategoryId;
            await _productRepository.UpdateAsync(updateProduct);
        }
    }
}
