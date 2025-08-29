using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Interface.InterfaceServices;
using TestWebApplication.Model;
using TestWebApplication.Services;

namespace TestWebApplication.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        /// <summary>
        /// Инициализирует новый экземпляр ProductController
        /// </summary>
        /// <param name="productServices">Сервис для работы с продуктами.</param>
        public ProductController(IProductServices productServices) { _productServices = productServices; }

        /// <summary>
        /// Получает все продукты.
        /// </summary>
        /// <returns>Коллекция всех продуктов.</returns>
        /// <response code="200">Возвращает список продуктов.</response>
        /// <response code="404">Если продукты не найдены.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductViewModel>> GetProduct()
        {
            try
            {
                var product = await _productServices.GetProductsTransferAsync();
                return Ok(product);
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (Exception ex) { return StatusCode(500, "Ошибка при запросе"); }
        }

        /// <summary>
        /// Получает продукт по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор продукта.</param>
        /// <returns>Продукт с указанным идентификатором</returns>
        /// <response code="200">Возвращает продукт.</response>
        /// <response code="404">Если продукт не найдены.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _productServices.GetProductById(id);
                return Ok(product);
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (Exception ex) { return StatusCode(500, "Ошибка при запросе"); }
        }

        /// <summary>
        /// Создает новый продукт.
        /// </summary>
        /// <param name="product">Данные продукта.</param>
        /// <returns>Созданный продукт.</returns>
        /// <response code="201">Возвращает созданный продукт.</response> 
        /// <response code="400">Если данные не предоставлены или некорректны.</response> 
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if(product == null) {  return BadRequest("Данные не введены"); }
            try
            {
                var create = await _productServices.PostProductAsync(product);
                return CreatedAtAction(nameof(CreateProduct), new { id = create.Id }, product);
            }
            catch (KeyNotFoundException) { return NotFound(); }
            catch (Exception ex) { return StatusCode(500, "Ошибка при запросе"); }
        }

        /// <summary>
        /// Обновляет существующий продукт.
        /// </summary>
        /// <param name="id">Идентификатор продукта для обновления.</param>
        /// <param name="product">Новые данные продукта.</param>
        /// <returns>Ничего не возвращает при успешном обновлении</returns>
        /// <response code="204">Продукт успешно обновлен.</response>
        /// <response code="400">Если идентификаторы не совпадают или данные некорректны.</response>
        /// <response code="404">Если продукт с указанным идентификатором не найден.</response>
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Данные продукта не предоставлены"); 
                if (id != product.Id) 
                    return BadRequest("ID не соответствует ID продукта");
                await _productServices.PutProductAsync(product);
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
        /// Удаляет продукт.
        /// </summary>
        /// <param name="id">Идентификатор продукта для удаления.</param>
        /// <returns>Ничего не возвращает при успешном удалении</returns>
        /// <response code="204">Продукт успешно удален.</response>
        /// <response code="404">Если продукт с указанным идентификатором не найден.</response> 
        /// <response code="500">При внутренней ошибке сервера.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productServices.GetProductById(id);
                if(product == null)
                {
                    return NotFound($"Продукт с ID {id} не найден");
                }
                else { await _productServices.DeleteProductAsync(id); }
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
