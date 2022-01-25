using ContosoPizza;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<ContosoPizzaContext>(d => d.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ContosoPizza;"));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IToppingService, ToppingService>();
builder.Services.AddDbContext<ContosoPizzaContext>((sp, options) => options.UseSqlServer(sp.GetRequiredService<IConfiguration>().GetConnectionString("Default")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
