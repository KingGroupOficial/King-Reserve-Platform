using KingReserveBack.PersonAdministration.Application.Internal.CommandServices;
using KingReserveBack.PersonAdministration.Application.Internal.QueryServices;
using KingReserveBack.PersonAdministration.Domain.Repositories;
using KingReserveBack.PersonAdministration.Domain.Services;
using KingReserveBack.PersonAdministration.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.ReserveAdministration.Domain.Repositories;
using KingReserveBack.ReserveAdministration.Domain.Services;
using KingReserveBack.ReserveAdministration.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.ReserveAdministration.Internal.CommandServices;
using KingReserveBack.ReserveAdministration.Internal.QueryServices;
using KingReserveBack.Shared.Domain.Repositories;
using KingReserveBack.Shared.Infrastructure.Interfaces.ASP.Configuration;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.StaffManagement.Application.Internal.CommandServices;
using KingReserveBack.StaffManagement.Application.Internal.QueryServices;
using KingReserveBack.StaffManagement.Domain.Repositories;
using KingReserveBack.StaffManagement.Domain.Services;
using KingReserveBack.StaffManagement.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Apply Route Naming Convention
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
    throw new Exception("Connection string is null");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString);
});

// OpenAPI/Swagger Configuration
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "King.Reserve.Backend.API",
                Version = "v1",
                Description = "King Group Platform API",
                // TermsOfService = new Uri("https://acme-learning.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "King Group",
                    Email = "maycoljhordan07@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
    });

//Add CORS Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


// Dependency Injection Configuration

// Shared Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ReserveAdministration Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IReserveRepository, ReserveRepository>();
builder.Services.AddScoped<IReserveCommandService, ReserveCommandService>();
builder.Services.AddScoped<IReserveQueryService, ReserveQueryService>();

// PersonAdministration Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonCommandService, PersonCommandService>();
builder.Services.AddScoped<IPersonQueryService, PersonQueryService>();

//StaffManagement Bounded Context Dependency Injections
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IStaffCommandService, StaffCommandService>();
builder.Services.AddScoped<IStaffQueryService, StaffQueryService>();

var app = builder.Build();



// Verify Database Objects are Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Apply CORS Policy
app.UseCors("AllowedAllPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();