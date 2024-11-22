using KingReserveBack.IAM.Application.Internal.CommandServices;
using KingReserveBack.IAM.Application.Internal.OutboundServices;
using KingReserveBack.IAM.Application.Internal.QueryServices;
using KingReserveBack.IAM.Domain.Model.Services;
using KingReserveBack.IAM.Domain.Repositories;
using KingReserveBack.IAM.Infrastructure.Hashing.BCrypt.Services;
using KingReserveBack.IAM.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using KingReserveBack.IAM.Infrastructure.Tokens.JWT.Configuration;
using KingReserveBack.IAM.Infrastructure.Tokens.JWT.Services;
using KingReserveBack.PersonAdministration.Application.Internal.CommandServices;
using KingReserveBack.PersonAdministration.Application.Internal.QueryServices;
using KingReserveBack.PersonAdministration.Domain.Repositories;
using KingReserveBack.PersonAdministration.Domain.Services;
using KingReserveBack.PersonAdministration.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.ReserveAdministration.Application.Internal.QueryServices;
using KingReserveBack.ReserveAdministration.Domain.Repositories;
using KingReserveBack.ReserveAdministration.Domain.Services;
using KingReserveBack.ReserveAdministration.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.ReserveAdministration.Internal.CommandServices;
using KingReserveBack.Shared.Domain.Repositories;
using KingReserveBack.Shared.Infrastructure.Interfaces.ASP.Configuration;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.StaffManagement.Application.Internal.CommandServices;
using KingReserveBack.StaffManagement.Application.Internal.QueryServices;
using KingReserveBack.StaffManagement.Domain.Repositories;
using KingReserveBack.StaffManagement.Domain.Services;
using KingReserveBack.StaffManagement.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.StaffManagement.Interfaces.Acl;
using KingReserveBack.StaffManagement.Interfaces.Services;
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
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "KING GROUP.KING RESERVE.API",
                Version = "v1",
                Description = "KING RESERVE Platform API",
                // TermsOfService = new Uri("https://acme-learning.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "KING RESERVE",
                    Email = "contact@pecuario.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
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

// IAM Bounded Context Injection Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
 builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();


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
//Add Authorization Middleware 
app.UseRequestAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();