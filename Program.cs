using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MediatrExample.Behaviours;
using MediatrExample.Domain;
using MediatrExample.Filters;
using MediatrExample.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// [Obsolete("Calling AddFluentValidation() is deprecated. Call services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters() instead, which has the same effect. For details see https://github.com/FluentValidation/FluentValidation/issues/1965 ")]

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(
    Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<MyAppDbContext>(optionsBuilder =>
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("default")));

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

SeedProducts();

app.Run();

async Task SeedProducts()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MyAppDbContext>();

    if (!context.Products.Any())
    {
        context.Products.AddRange(new List<Product>
        {
            new Product
            {
                Description = "Product 01",
                Price = 16000
            },
            new Product
            {
                Description = "Product 02",
                Price = 52200
            }
        });

        await context.SaveChangesAsync();
    }
}