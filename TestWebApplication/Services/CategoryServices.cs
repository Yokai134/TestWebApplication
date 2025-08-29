using Microsoft.EntityFrameworkCore;
using TestWebApplication.Interface.InterfaceRepository;
using TestWebApplication.Interface.InterfaceServices;
using TestWebApplication.Model;

namespace TestWebApplication.Services
{
    /// <summary>
    /// Сервис для работы с бизнес-логикой категорий.
    /// </summary>
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Инициализирует новый экземпляр CategoryServices.
        /// </summary>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public CategoryServices(ICategoryRepository categoryRepository) { _categoryRepository = categoryRepository; }

        /// <summary>
        /// Удаляет категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <exception cref="KeyNotFoundException">Выбрасывается если категория не найдена.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Выбрасывается, если возник конфликт параллельного доступа при удалении.</exception>
        /// <exception cref="Exception">Выбрасывается, если не удалось разрешить конфликт параллельного доступа.</exception>
        public async Task DeleteCategoryAsync(int id)
        {
            var deleteCategory = await _categoryRepository.GetAsyncById(id);
            if (deleteCategory == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            try
            {
                await _categoryRepository.DeleteAsync(deleteCategory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var resourceStillExists = await _categoryRepository.GetAsyncById(id) != null;

                if (!resourceStillExists)
                {
                    return;
                }

                throw new Exception("Не удалось удалить ресурс из-за конфликта версий. Попробуйте еще раз.", ex);
            }
        }

        /// <summary>
        /// Получает список категорий.
        /// </summary>
        /// <returns>Отсортированниый список с категориями.</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывает если список пуст.</exception>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if(categories == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            return categories.OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// Получает категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Возвращает категорию.</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывает если категория не найдена.</exception>
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var categories =  await _categoryRepository.GetAsyncById(id);
            if (categories == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            return categories;
        }

        /// <summary>
        /// Получаем список продуктов которые принадлежат указанной категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Возвращает список продуктов принадлежащие указанной категории</returns>
        /// <exception cref="KeyNotFoundException">Выбрасывает если продукты не найдена.</exception>
        public async Task<IEnumerable<Product>> GetProductsAsync(int id)
        {
            var product = await _categoryRepository.GetByCategoryId(id);

            if(product == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }

            return product.OrderBy(x=>x.Id).ToList();
        }

        /// <summary>
        /// Добавляем новую категорию
        /// </summary>
        /// <param name="category">Данные категории.</param>
        /// <returns>Добавленная категория с присвоенным идентификатором.</returns>
        public async Task<Category> PostCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return category;
        }

        /// <summary>
        /// Обновляет данные категории.
        /// </summary>
        /// <param name="category">Измененные данные категории.</param>
        /// <exception cref="KeyNotFoundException">Выбрасывает если категория не найдена.</exception>
        public async Task PutCategoryAsync(Category category)
        {
            var updateCategory = await _categoryRepository.GetAsyncById(category.Id);
            if (updateCategory == null)
            {
                throw new KeyNotFoundException("Данные не найдены");
            }
            updateCategory.Categoryname = category.Categoryname;
            await _categoryRepository.UpdateAsync(updateCategory);
        }
    }
}
