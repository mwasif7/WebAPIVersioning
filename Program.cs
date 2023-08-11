using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.IdentityModel.Tokens;
using WebAPIVersioning;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

var config = builder.Configuration;
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

    // #Instead of decorators on controller we can use below code to give the API version to the controllers
    // option.Conventions.Controller<WeatherForecast>()
    //             .HasDeprecatedApiVersion(1,0)
    //             .HasApiVersion(2,0);


    option.ReportApiVersions = true;
});

builder.Services.AddAuthentication( options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme=  JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer( x =>
{
    x.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer= true,
        ValidateAudience= true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
    };
}) ;

builder.Services.AddMvc();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
