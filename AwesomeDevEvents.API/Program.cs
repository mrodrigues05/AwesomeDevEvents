using AwesomeDevEvents.API.Mappers;
using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("DevEventsCs");

//builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseInMemoryDatabase("DevEventsDb"));

builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseSqlServer(connectionstring));

builder.Services.AddAutoMapper(typeof(DevEventProfile).Assembly);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AwesomwDevEvents.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Matheus Rodrigues",
            Email = "mmrodrigues077@gamil.com",
            Url = new Uri("https://www.linkedin.com/in/mrodrigues077/")
        }
    });

    var xmlFile = "AwesomeDevEvents.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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

app.Run();
