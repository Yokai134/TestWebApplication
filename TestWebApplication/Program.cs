using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TestWebApplication.Data;
using TestWebApplication.Interface.InterfaceRepository;
using TestWebApplication.Interface.InterfaceServices;
using TestWebApplication.Repository;
using TestWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// ===== Õ¿—“–Œ… ¿ ¡¿«€ ƒ¿ÕÕ€’ =====
builder.Services.AddDbContext<AsdfgContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors());

// ===== Õ¿—“–Œ… ¿ —≈–¬»—Œ¬ =====
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();

// ===== Õ¿—“–Œ… ¿ SWAGGER =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Õ¿—“–Œ… ¿ CORS ƒÀﬂ BLAZOR =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7176") // Blazor WASM
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ===== Õ¿—“–Œ… ¿ JSON —≈–»¿À»«¿÷»» =====
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
var app = builder.Build();

// =====  ŒÕ‘»√”–¿÷»ﬂ PIPELINE =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
