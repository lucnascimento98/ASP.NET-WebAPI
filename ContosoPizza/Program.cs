using ContosoPizza;
using ContosoPizza.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using ContosoPizza.PipelineBehaviors;
using Nudes.Retornator.AspnetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ContosoPizza.Services;

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
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
builder.Services.AddSingleton<TokenService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Authentication").GetValue<string>("JWTKey").ToString());
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();