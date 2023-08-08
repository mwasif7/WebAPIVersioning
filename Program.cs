using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified= true;
    option.DefaultApiVersion = ApiVersion.Default;
    // #different ways to add version in API
    // option.ApiVersionReader = new MediaTypeApiVersionReader("version");
    // option.ApiVersionReader = new HeaderApiVersionReader("api-version"); 
    // option.ApiVersionReader = ApiVersionReader.Combine(
    //     new MediaTypeApiVersionReader("version"),
    //     new HeaderApiVersionReader("apiVersion")
    // );
    option.ReportApiVersions = true;

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
