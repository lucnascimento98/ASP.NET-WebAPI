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
using Mapster;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<ContosoPizzaContext>(d => d.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ContosoPizza;"));

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.local.json", true, true);
builder.Services.AddControllers().AddRetornator();
builder.Services.AddErrorTranslator(ErrorHttpTranslatorBuilder.Default);
builder.Services.AddDbContext<ContosoPizzaContext>((sp, options) => options.UseSqlServer(sp.GetRequiredService<IConfiguration>().GetConnectionString("Default")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetEntryAssembly());
builder.Services.AddHttpContextAccessor();

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileWithDebugInfo();


builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
builder.Services.AddSingleton<TokenService>();

builder.Services.AddAuthorization(options =>
{
    
    options.AddPolicy("AddPizza", policy => policy.RequireClaim("Claim", "AddPizza"));
    options.AddPolicy("EditPizza", policy => policy.RequireClaim("Claim", "EditPizza"));
    options.AddPolicy("DeletePizza", policy => policy.RequireClaim("Claim", "DeletePizza"));
    options.AddPolicy("AddTopping", policy => policy.RequireClaim("Claim", "AddTopping"));
    options.AddPolicy("EditTopping", policy => policy.RequireClaim("Claim", "EditTopping"));
    options.AddPolicy("DeleteTopping", policy => policy.RequireClaim("Claim", "DeleteTopping"));
    options.AddPolicy("AddUserAdmin", policy => policy.RequireClaim("Claim", "AddUserAdmin")); //duvida
    options.AddPolicy("EditUser", policy => policy.RequireClaim("Claim", "EditUser")); //duvida
    options.AddPolicy("GetUser", policy => policy.RequireClaim("Claim", "GetUser")); 
    options.AddPolicy("GetAllUser", policy => policy.RequireClaim("Claim", "GetAllUser"));
    options.AddPolicy("DeleteUser", policy => policy.RequireClaim("Claim", "DeleteUser")); //duvida
    options.AddPolicy("ChangePassword", policy => policy.RequireClaim("Claim", "ChangePassword")); 
    options.AddPolicy("AddRole", policy => policy.RequireClaim("Claim", "AddRole")); 
    options.AddPolicy("EditRole", policy => policy.RequireClaim("Claim", "EditRole"));
    options.AddPolicy("GetRole", policy => policy.RequireClaim("Claim", "GetRole"));
    options.AddPolicy("GetAllRole", policy => policy.RequireClaim("Claim", "GetAllRole"));
    options.AddPolicy("DeleteRole", policy => policy.RequireClaim("Claim", "DeleteRole"));
    options.AddPolicy("AddClaimToRole", policy => policy.RequireClaim("Claim", "AddClaimToRole"));
    options.AddPolicy("RemoveClaimFromRole", policy => policy.RequireClaim("Claim", "RemoveClaimFromRole"));
    options.AddPolicy("ListRoleClaims", policy => policy.RequireClaim("Claim", "ListRoleClaims"));
    options.AddPolicy("GetAllUsersOrders", policy => policy.RequireClaim("Claim", "GetAllUsersOrders"));
    options.AddPolicy("OrderPizza", policy => policy.RequireClaim("Claim", "OrderPizza"));
});


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