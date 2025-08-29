using System;
using System.Collections.Generic;

namespace TestWebApplication.Model;

/// <summary>
/// Представляет категорию товаров в системе.
/// </summary>
public partial class Category
{
    /// <summary>
    /// Уникальный идентификатор категории.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование категории. Обязательное поле.
    /// </summary>
    public string Categoryname { get; set; } = null!;
}
