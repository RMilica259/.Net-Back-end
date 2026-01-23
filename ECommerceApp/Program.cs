using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.IServices;
using ECommerceApp.Application.Services;
using ECommerceApp.Application.UseCases.Commands.CreateOrder;
using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Domain.Date;
using ECommerceApp.Infrastructure;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Queries;
using ECommerceApp.Infrastructure.Repository;
using ECommerceApp.Web.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddMediatRServices();

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

public partial class Program { }
