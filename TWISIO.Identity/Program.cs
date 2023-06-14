using Microsoft.AspNetCore.Identity;
using TWISIO.Identity.API.Middlewares.ExceptionMiddleware;
using TWISIO.Identity.API.Services;
using TWISIO.Identity.Application;
using TWISIO.Identity.Application.DTOs;
using TWISIO.Identity.Domain;
using TWISIO.Identity.Persistence;

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

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<DBContext>();

builder.Services.AddAuthenticationService(jwtOptions);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPersistenceService(connectionString, builder.Configuration);
builder.Services.AddApplication(jwtOptions);
builder.Services.AddSwaggerService();

var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
