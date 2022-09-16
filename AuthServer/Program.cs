using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AuthServer.Repositories;
using AuthServer.Services;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Uncomment below to customize logging (un-redact certain log messages) */

// builder.Services.AddHttpLogging(logging =>
// {
//     logging.LoggingFields = HttpLoggingFields.All;
//     logging.RequestHeaders.Add("Authorization");
//     logging.ResponseHeaders.Add("Authorization");
//     
//     logging.MediaTypeOptions.AddText("application/javascript");
// });

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],    
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 // CORS settings
 app.UseCors(corsBuilder =>
 {
     corsBuilder.WithOrigins("http://localhost:3000", "https://localhost:8080", "http://localhost:8080");
     corsBuilder.AllowAnyMethod();
     corsBuilder.AllowAnyHeader();
     corsBuilder.AllowCredentials();
 });

app.UseCors();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
