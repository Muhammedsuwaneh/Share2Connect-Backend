using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Share2Connect.Backend.Context;
using Share2Connect.Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string ApiCorsPolicy = "_apiCorsPolicy";

builder.Services.AddControllers();

// CORS config
builder.Services.AddCors(options => options.AddPolicy(ApiCorsPolicy, builder =>
{
    builder.WithOrigins("https://localhost:7195", "https://localhost:5195")
    .AllowAnyHeader()
    .AllowAnyMethod().AllowAnyOrigin();
}));


// JWT Auth Config

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
    };  
});

// setup sql server service config for user db context db migration
builder.Services.AddDbContext<UserDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

SeedInitializer.seed(app);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(ApiCorsPolicy);

app.Run();