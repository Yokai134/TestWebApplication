using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestWebApplication.Model;

namespace TestWebApplication.Data;

/// <summary>
/// Представляет сессию взаимодействия с базой данных и предоставляет доступ к её таблицам (сущностям).
/// Является центральным классом в Entity Framework Core для операций CRUD (Create, Read, Update, Delete).
/// </summary>
/// <remarks>
/// Этот класс был сгенерирован шаблоном EF Core и частично реализован вручную.
/// Для подключения к БД использует PostgreSQL (Npgsql).
/// </remarks>
public partial class AsdfgContext : DbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AsdfgContext"/> без настроек.
    /// Настройки должны быть предоставлены через <see cref="OnConfiguring"/>.
    /// </summary>
    public AsdfgContext()
    {
        throw new InvalidOperationException(
            "Контекст не должен создаваться через конструктор без параметров. " +
            "Используйте конструктор с DbContextOptions или DI."
        );
    }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AsdfgContext"/> с указанными параметрами.
    /// </summary>
    /// <param name="options">Параметры, используемые для настройки этого контекста.</param>
    public AsdfgContext(DbContextOptions<AsdfgContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Представляет таблицу "Category" в базе данных.
    /// Используется для запросов, сохранения и обновления данных о категориях.
    /// </summary>
    public virtual DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Представляет таблицу "Product" в базе данных.
    /// Используется для запросов, сохранения и обновления данных о продуктах.
    /// </summary>
    public virtual DbSet<Product> Products { get; set; }

    /// <summary>
    /// Настраивает модель базы данных, включая связи между сущностями, ограничения, индексы и т.д.
    /// Этот метод вызывается при создании модели для данного контекста.
    /// </summary>
    /// <param name="modelBuilder">Построитель, используемый для конфигурации модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(100)
                .HasColumnName("categoryname");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            // Пример комментария для конкретной конфигурации
            // <summary>Индекс для оптимизации запросов по полю CategoryId.</summary>
            entity.HasIndex(e => e.CategoryId, "idx_product_category").HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "false");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Productname)
                .HasMaxLength(100)
                .HasColumnName("productname");
            entity.Property(e => e.Productcount).HasColumnName("productcount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    /// <summary>
    /// Частичный метод, позволяющий добавить дополнительную конфигурацию модели в другом файле.
    /// Это предотвращает перезапись пользовательского кода при повторной генерации шаблоном.
    /// </summary>
    /// <param name="modelBuilder">Построитель, используемый для конфигурации модели.</param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
