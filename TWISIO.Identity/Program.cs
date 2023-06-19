using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TWISIO.Identity.API.Common.Managers;
using TWISIO.Identity.API.Common.Options;
using TWISIO.Identity.API.Common.Services;
using TWISIO.Identity.API.Data;
using TWISIO.Identity.API.Data.Services;
using TWISIO.Identity.API.DTOs;
using TWISIO.Identity.API.Entities;
using TWISIO.Identity.API.Interfaces;
using TWISIO.Identity.API.Interfaces.Repositories;
using TWISIO.Identity.API.Middlewares.ExceptionMiddleware;
using TWISIO.Identity.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreSQL")!;

var jwtOptions = new JwtOptionsDto
{
    RefreshTokenExpires = TimeSpan.FromDays(30),
    EXPIRES = TimeSpan.FromMinutes(30),
    ISSUER = builder.Configuration["JWT:Issuer"]!,
    AUDIENCE = builder.Configuration["JWT:Audience"]!,
    KEY = builder.Configuration["JWT:SecretKey"]!
};

var emailSenderOptions = new EmailSenderOptions
{
    Name = builder.Configuration["SMTP:Name"],
    Username = builder.Configuration["SMTP:Username"],
    Password = builder.Configuration["STMP:Password"],
    Port = Convert.ToInt32(builder.Configuration["SMTP:Port"]),
    Host = builder.Configuration["SMTP:Host"]
};

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<DBContext>();

builder.Services.AddAuthenticationService(jwtOptions);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<DBContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IDBContext>(provider => provider.GetService<DBContext>()!);
builder.Services.AddScoped<IEmailSender>(x => new EmailSender(emailSenderOptions));
builder.Services.AddScoped<IFileUploader, FileUploader>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenManager>(x => new TokenManager(jwtOptions));

builder.Services.AddSwaggerService();
builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
}));

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<DBContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception e)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, $"Произошла ошибка при инициализации базы данных: {e.Message}");
    }
}

app.UseExceptionMiddleware();

app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
