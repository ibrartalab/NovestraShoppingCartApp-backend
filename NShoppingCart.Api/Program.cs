using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NovestraTodo.Core.Interfaces;
using NShoppingCart;
using NShoppingCart.Api.Middlewars;
using NShoppingCart.Application.Services.Implementation;
using NShoppingCart.Application.Services.Implementations;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Interfaces.Services;
using NShoppingCart.Infrastructure.Data;
using NShoppingCart.Infrastructure.ExternalServices.JwtGeneration;
using NShoppingCart.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- Standard API Services ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Dependency Injection: Repositories ---
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>(); // Added this
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// --- Dependency Injection: Services ---
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();       // Added this
builder.Services.AddScoped<IOrderService, OrderService>();

// --- Database Configuration ---
var connectionString = builder.Configuration.GetConnectionString("NShoppingCartConnection");
builder.Services.AddDbContext<NShoppingCartDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("NShoppingCart.Infrastructure")));

// --- CORS Configuration ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// --- Authentication & Authorization ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// --- Middleware Pipeline ---
app.UseCors("AllowLocalhost5173");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();