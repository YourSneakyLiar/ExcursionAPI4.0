using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using DataAccess.Wrapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Wrapper;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Domain.Interfacess;
using Microsoft.AspNetCore.Hosting;
using BackendApi.Authorization;
using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using BackendApi.Helpers;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Mapster;

public class Program
{
    public static async Task Main(string[] args) // Изменен тип возвращаемого значения на Task и добавлен async
    {
        Console.Title = "IdentityServer";

        var builder = WebApplication.CreateBuilder(args);

        // Настройка строки подключения
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ExcursionBdContext>(options => options.UseSqlServer(connectionString));

        // configure strongly typed settings object
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        // В методе ConfigureServices или в Program.cs
        builder.Services.AddMapster(); // Регистрация Mapster
        builder.Services.AddSingleton<IMapper>(new Mapper()); // Регистрация IMapper

        // configure DI for application services
        builder.Services.AddScoped<IJwtUtils, JwtUtils>();
        builder.Services.AddScoped<IAccountService, AccountsService>();
        builder.Services.AddScoped<IEmailService, EmailService>();

        // Регистрация сервисов
        builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IComplaintService, ComplaintService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProviderServiceService, ProviderServiceService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IStatisticService, StatisticService>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddScoped<ITourLoadStatisticService, TourLoadStatisticService>();
        builder.Services.AddScoped<ITourService, TourService>();


        // Настройка Swagger
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Экскурсионное-бюро API",
                Description = "Экскурсии по России API",
                Contact = new OpenApiContact
                {
                    Name = "Пример контакта",
                    Url = new Uri("https://gorbilet.com/msk/catalog/ekskursii-po-moskve/")
                },
                License = new OpenApiLicense
                {
                    Name = "Пример лицензии",
                    Url = new Uri("https://example.com/license")
                }
            });

            // Добавление настроек безопасности для JWT
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            // Добавление требования безопасности для всех эндпоинтов
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            // Подключение XML-комментариев
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        // Добавление контроллеров и Swagger
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Настройка CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Логирование
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();

        var app = builder.Build();

        // Middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthorization();

        // Global error handler
        app.UseMiddleware<ErrorHandlerMiddleware>();

        // Custom JWT auth middleware
        app.UseMiddleware<JwtMiddleware>();

        app.MapControllers();

        // Конфигурация Kestrel
        app.Urls.Add("http://localhost:5000");

        // Миграция базы данных
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetService<ExcursionBdContext>();
            await context.Database.MigrateAsync(); // Теперь этот код работает корректно
        }
        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        //if (string.IsNullOrEmpty(connectionString))
        //{
        //    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        //}

        Console.WriteLine($"Connection String: {connectionString}");

        await app.RunAsync(); // Изменен вызов Run на RunAsync
    }
}