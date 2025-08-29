namespace TestWebApplication.Interface
{
    /// <summary>
    /// Определяет базовые операции CRUD (Create, Read, Update, Delete) для репозиториев.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>Получить все сущности типа T.</summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>Получить сущность по идентификатору.</summary>
        /// <param name="id">Идентификатор сущности.</param>
        Task<T> GetAsyncById(int id);

        /// <summary>Добавить новую сущность.</summary>
        /// <param name="entity">Сущность для добавления.</param>
        Task AddAsync(T entity);

        /// <summary>Обновить существующую сущность.</summary>
        /// <param name="entity">Сущность с обновленными данными.</param>
        Task UpdateAsync(T entity);

        /// <summary>Удалить сущность.</summary>
        /// <param name="entity">Сущность для удаления.</param>
        Task DeleteAsync(T entity);
    }
}
