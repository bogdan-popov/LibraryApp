using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Services;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LibraryApp.DataAccess.LibraryDbContext>(options => options.UseNpgsql(connectionString));

// –егистрирую через AddScoped, чтобы один экземпл€р создавалс€ дл€ каждого HTTP-запроса и использовалс€ всеми сервисами в рамках этого запроса
// Ёто гарантирует, что все операции в рамках одного запроса будут частью одной транзакции
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();