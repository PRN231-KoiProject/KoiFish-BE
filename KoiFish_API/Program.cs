using AutoMapper.Internal;
using AutoMapper;
using KoiFish_API.Extensions;
using KoiFish_API.GlobalExceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using KoiFish_API.AutoMapper;
using KoiFish_API;
using KoiFish_Core.Repositories;
using KoiFish_Data.Repositories;
using KoiFish_Core.Services;
using KoiFish_Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlContext(configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureIdentity();
builder.Services.AddExceptionHandler<GlobalExceptionHanders>();
builder.Services.ConfigureJwtSetting(builder.Configuration);
builder.Services.ConfigureTokenAndManagerIdentity();
builder.Services.AddCustomJwtAuthentication(configuration);

// DI
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IColorRepository,ColorRepository>();
builder.Services.AddScoped<IColorService,ColorService>();
builder.Services.AddScoped<IKoiFishRepository,KoiFishRepository>();
builder.Services.AddScoped<IKoiFishService,KoiFishService>();
builder.Services.AddScoped<IKoiFishColorRepository,KoiFishColorRepository>();
builder.Services.AddScoped<IImageRepository,ImageRepository>();


// JWT
builder.Services.AddSwaggerGen(option =>
{


    option.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          },
            In = ParameterLocation.Header,
                Scheme = "Bearer",
                Name = "Authorization",
                BearerFormat = "JWT"
        },
        Array.Empty<string>()
      }
    });
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.Internal().MethodMappingEnabled = false;
    mc.AddProfile(new MappingProfiles());
});
builder.Services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrationDatabase();
app.Run();
