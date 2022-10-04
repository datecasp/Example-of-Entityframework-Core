using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Extensions;
using Example_of_Entityframework_Core.Services;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Add Connection to SQL Server (SQLEXPRESS)
const string CONNECTIONSTRING = "EF";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONSTRING);

//Add Context (DB) to Services of builder
builder.Services.AddDbContext<EntityDBContext>(options => options.UseSqlServer(connectionString));

// Add JWT authorization
builder.Services.AddJwtTokenServices(builder.Configuration);


// Add services to the container.

builder.Services.AddControllers();

//Add custom Services
builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
builder.Services.AddScoped<ILibroServices, LibroServices>();   
builder.Services.AddScoped<ICategoriaServices, CategoriaServices>();

// Add Authorization
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // We define the security for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using bearer scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
                new OpenApiSecurityScheme
                {
                    Reference= new OpenApiReference
                    {
                    Type= ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                },
            new string[]{ }
        }
    });
});

// Cors Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.Run();
