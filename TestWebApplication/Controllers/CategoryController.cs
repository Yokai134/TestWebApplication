using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Interface.InterfaceServices;
using TestWebApplication.Model;

namespace TestWebApplication.Controllers
{
    /// <summary>
    /// Контроллер для управления категориями товаров.
    /// </summary>
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        /// <summary>
        /// Инициализирует новый экземпляр CategoryController.
        /// </summary>
        /// <param name="categoryServices">Сервис для работы с категориями.</param>
        public CategoryController(ICategoryServices categoryServices) { _categoryServices = categoryServices; }


        /// <summary>
        /// Получает все категории товаров.
        /// </summary>
        /// <returns>Коллекция всех категорий.</returns>
        /// <response code="200">Возвращает список категорий.</response>
        /// <response code="404">Если категории не найдены.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> GetCategory()
        {
            try
            {
                var category = await _categoryServices.GetCategoriesAsync();
                return Ok(category);
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (Exception ex) { return StatusCode(500, "Ошибка при запросе"); }
        }

        /// <summary>
        /// Получает категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Категория с указанным идентификатором.</returns>
        /// <response code="200">Возвращает найденную категорию.</response>
        /// <response code="404">Если категория не найдена.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryServices.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (Exception ex) { return StatusCode(500, "Ошибка при запросе"); }
        }

        /// <summary>
        /// Создает новую категорию.
        /// </summary>
        /// <param name="category">Данные новой категории.</param>
        /// <returns>Созданная категория.</returns>
        /// <response code="201">Возвращает созданную категорию.</response>
        /// <response code="400">Если данные не предоставлены или некорректны.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            if (category == null) { return BadRequest("Данные не введены"); }
            try
            {
                var create = await _categoryServices.PostCategoryAsync(category);
                return CreatedAtAction(nameof(CreateCategory), new { id = create.Id }, create);
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (Exception ex) { return StatusCode(500, "Ошибка при запросе"); }
        }

        /// <summary>
        /// Обновляет существующую категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории для обновления.</param>
        /// <param name="category">Новые данные категории.</param>
        /// <returns>Ничего не возвращает при успешном обновлении.</returns>
        /// <response code="204">Категория успешно обновлена.</response>
        /// <response code="400">Если идентификаторы не совпадают или данные некорректны.</response>
        /// <response code="404">Если категория с указанным идентификатором не найдена.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if(id != category.Id)
                    return BadRequest("ID не найден");
                await _categoryServices.PutCategoryAsync(category);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ошибка при запросе");
            }
        }

        /// <summary>
        /// Удаляет категорию, если она не используется в продуктах.
        /// </summary>
        /// <param name="id">Идентификатор категории для удаления.</param>
        /// <returns>Ничего не возвращает при успешном удалении.</returns>
        /// <response code="204">Категория успешно удалена.</response>
        /// <response code="400">Если категория используется в продуктах и не может быть удалена.</response>
        /// <response code="404">Если категория с указанным идентификатором не найдена.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
               var products = await _categoryServices.GetProductsAsync(id);
                if (products.Any())
                {
                    return BadRequest("Категория используется в продуктах и не может быть удалена");
                }

                var category = await _categoryServices.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound($"Категория с ID {id} не найдена");
                }

                await _categoryServices.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ошибка при запросе");
            }
        }
    }
}
