using Application;
using Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Infrastructure.Filters;
using FluentValidation.AspNetCore;
using Application.Features.Customers.Commands.Rules;
using SalesManagement.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

#region Custom Service Configuration
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(configuration);
#endregion

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration
    .RegisterValidatorsFromAssemblyContaining<CreateCustomerValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = false);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc();
builder.Services.AddSwaggerGen(configuration =>
{
    configuration.SwaggerDoc("v1", new OpenApiInfo() { Title = Assembly.GetEntryAssembly().GetName().Name, Version = "v1" });

    var xmlFile = "SalesManagement.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    configuration.IncludeXmlComments(xmlPath);
});

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = false; });

var app = builder.Build();

app.UseCors(q => q.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.API v1"));
}

app.UseExceptionHandler(s => s.Run(ExceptionHandler.Handle));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();