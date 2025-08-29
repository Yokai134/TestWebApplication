using Microsoft.EntityFrameworkCore;
using TestWebApplication.Data;
using TestWebApplication.Interface;

namespace TestWebApplication.Repository
{
    /// <summary>
    /// Базовая реализация репозитория, предоставляющая CRUD-операции для сущностей.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий.</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly AsdfgContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр BaseRepository.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public BaseRepository(AsdfgContext context) { _context = context; }

        /// <summary>
        /// Добавляет новую запись в базу данных.
        /// </summary>
        /// <param name="entity">Данные для добавления.</param>
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет запись из базы данных.
        /// </summary>
        /// <param name="entity">Данные для удаления.</param>
        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает все сущности указанного типа.
        /// </summary>
        /// <returns>Коллекция всех сущностей.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Получает запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная запись или null.</returns>
        public async Task<T> GetAsyncById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Обновляет существующую запись в базе данных.
        /// </summary>
        /// <param name="entity">Обновленные данные.</param>
        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
