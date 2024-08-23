
using SB.PruebaTecnica.Application.Commands.GovernmentEntities;
using SB.PruebaTecnica.Application.Interfaces;
using SB.PruebaTecnica.Domain.Interfaces;
using SB.PruebaTecnica.Infrastructure.Repositories;
using SB.PruebaTecnica.Infrastructure.Security;
using System.Reflection;
using MediatR;
using SB.PruebaTecnica.Application.Handlers.Users;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using log4net.Config;
using log4net;
using SB.PruebaTecnica.API;

var builder = WebApplication.CreateBuilder(args);

var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CreateUserCommandHandler)));
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(AuthenticateUserCommandHandler)));
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(AddGovernmentEntityCommand)));
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetAllGovernmentEntitiesCommand)));
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetGovernmentEntityByIdCommand)));
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(UpdateGovernmentEntityCommand)));
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(DeleteGovernmentEntityCommand)));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGovernmentEntityRepository, GovernmentEntityRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<ILog>(LogManager.GetLogger(typeof(Program)));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");

builder.Services.AddSingleton<ITokenService>(new JwtTokenService(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SB API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var app = builder.Build();

app.UseMiddleware<ErrorLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SB API V1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder => 
            builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
