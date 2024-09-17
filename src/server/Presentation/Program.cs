using Application.Configuration;
using Infrastructure;
using Infrastructure.Database.Context;
using Microsoft.OpenApi.Models;
using Presentation.Extensions;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Presentation layer. Controllers
builder.Services.AddControllers();

// Application layer.
builder.Services.ConfigureApplicationServices();

// Infrastructure layer.
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Request JWT token in Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "yt-tracker API", Version = "v1" });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

    options.CustomSchemaIds(type => type.ToString());
});

// JWT configuration
builder.AddMySecretConfiguration();
builder.AddJwtAuthentication();

var app = builder.Build();

// Ensure database is created.
CreateDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void CreateDatabase(WebApplication app)
{
    var serviceScope = app.Services.CreateScope();
    var dataContext = serviceScope.ServiceProvider.GetService<MyDbContext>();
    dataContext?.Database.EnsureDeleted();
    dataContext?.Database.EnsureCreated();
}