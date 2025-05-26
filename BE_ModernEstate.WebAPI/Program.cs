using System.Text.Json.Serialization;
using BE_ModernEstate.WebAPI.Configurations;
using BE_ModernEstate.WebAPI.Configurations.BrowserProvider;
using BE_ModernEstate.WebAPI.Middlewares;
using BE_ModernEstate.WebAPI.WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModernEstate.Common.Config;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Settings;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbSection = builder.Configuration.GetSection("Database");
var server = dbSection["Server"];
var port = dbSection["Port"];
var database = dbSection["DataName"];
var user = dbSection["UserId"];
var password = dbSection["Password"];

var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? server;
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? port;
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? database;
var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? user;
var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? password;

var connectionString =
    $"Server={dbHost};Port={dbPort};Database={dbName};User Id={dbUser};Password={dbPass};SslMode=Required;";

builder.Services.AddDbContext<ApplicationDbConext>(options =>
{
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 31)),
        mySqlOptions =>
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            )
    );
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddJwtAuthentication(builder.Configuration);

// Gọi cấu hình DI
builder.Services.AddProjectDependencies();
builder.Services.AddSwaggerDependencies();
builder.Services.AddAutoMapperConfiguration();
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSingleton<IBrowserProvider, PuppeteerBrowserProvider>();
builder.Services.Configure<VNPayConfiguration>(builder.Configuration.GetSection("VNPay"));
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowExpoApp",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});
var app = builder.Build();

app.Lifetime.ApplicationStopped.Register(async () =>
{
    // Đảm bảo đóng browser khi ứng dụng dừng
    var provider = app.Services.GetRequiredService<IBrowserProvider>();
    await provider.DisposeAsync();
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbConext>();
    db.Database.Migrate();

    // Seed roles
    foreach (var name in Enum.GetNames<EnumRoleName>())
    {
        var roleEnum = Enum.Parse<EnumRoleName>(name);
        if (!db.Roles.Any(r => r.RoleName == roleEnum))
            db.Roles.Add(new Role { RoleName = roleEnum });
    }
    db.SaveChanges();

    // Seed admin user nếu chưa có
    var adminRole = db.Roles.Single(r => r.RoleName == EnumRoleName.ROLE_ADMIN);
    bool exists = db.Accounts.Any(u => u.RoleId == adminRole.Id);
    if (!exists)
    {
        var admin = new Account
        {
            Email = "admin@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("123456789"),
            RoleId = adminRole.Id,
            EnumAccountStatus = EnumAccountStatus.ACTIVE,
        };
        db.Accounts.Add(admin);
        db.SaveChanges();
    }
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();

// }

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowExpoApp");

app.MapControllers();

app.Run();
