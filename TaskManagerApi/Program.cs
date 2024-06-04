using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using TaskManager.Common.Extensions;
using TaskManager.Core.Application;
using TaskManager.Core.Application.Dtos;
using TaskManager.Infrastructure.Persistence;
using TaskManager.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddServices();
builder.Services.AddDtoMaping();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

var cultureInfo = new CultureInfo("es-DO");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerExtensions();
builder.Services.AddApiVersioningExtension();




builder.Services.AddCors(o =>
    o.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));



#region Inyection

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddSingleton(appSettings);

#endregion


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = appSettings.Domain,
            ValidAudience = appSettings.Domain,
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(appSettings.TokenKey)),
            ClockSkew = TimeSpan.Zero,
        });


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors("CorsPolicy");

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtensions();
app.MapControllers();

app.Run();
