using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//var configValue = builder.Configuration.GetValue<string>("Authentication:CookieAuthentication:LoginPath");

var _Key = builder.Configuration.GetValue<string>("TokenKey");


// add token service (custom build to the services). good example for dependency injection

// this code is moved to Application service Extensions

// builder.Services.AddScoped<ITokenService,TokenService>();
// // Add services to the container.
// builder.Services.AddDbContext<DataContext>(options =>{
//     options.UseSqlite(connectionString);
// });
builder.Services.AddApplicationServices(connectionString);
builder.Services.AddCors();

// moved to Identity Services
builder.Services.AddIdentityServices(_Key);
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer(options => {
//     options.TokenValidationParameters = new TokenValidationParameters{
        
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Key)),
//         ValidateIssuer = false,
//         ValidateAudience = false,
//     };
//     });
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



app.Run();
