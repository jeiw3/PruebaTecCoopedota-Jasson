using Microsoft.EntityFrameworkCore;
using usersApi.CasosDeUso;
using usersApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//pasando todas las rutas a minuscula por si acaso

builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
builder.Services.AddDbContext<UserDatabaseContext>(mysqlBuilder =>
{
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
