using ContosoPizza;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using ContosoPizza.PipelineBehaviors;
using Nudes.Retornator.AspnetCore;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<ContosoPizzaContext>(d => d.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ContosoPizza;"));

// Add services to the container.

builder.Services.AddControllers().AddRetornator();
builder.Services.AddErrorTranslator(ErrorHttpTranslatorBuilder.Default);
builder.Services.AddDbContext<ContosoPizzaContext>((sp, options) => options.UseSqlServer(sp.GetRequiredService<IConfiguration>().GetConnectionString("Default")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetEntryAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());

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