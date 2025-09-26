using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LibraryApp.DataAccess.LibraryDbContext>(options => options.UseNpgsql(connectionString));

// –егистрирую через AddScoped, чтобы один экземпл€р UnitOfWork создавалс€ дл€ каждого HTTP-запроса и использовалс€ всеми сервисами в рамках этого запроса
// Ёто гарантирует, что все операции в рамках одного запроса будут частью одной транзакции
builder.Services.AddScoped<LibraryApp.DataAccess.Interfaces.IUnitOfWork, LibraryApp.DataAccess.Repositories.UnitOfWork>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();