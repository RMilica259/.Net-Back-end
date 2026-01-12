using ECommerceApp.Application.IRepository;
using ECommerceApp.Application.UseCases.Commands.CreateOrder;
using ECommerceApp.Application.UseCases.Queries.GetCartItem;
using ECommerceApp.Domain.Date;
using ECommerceApp.Application.IServices;
using ECommerceApp.Application.Services;
using ECommerceApp.Infrastructure;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Queries;
using ECommerceApp.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IGetCartQuery, GetCartQuery>();

builder.Services.AddScoped<StockAvailability>();
builder.Services.AddScoped<IStockAvailability, StockAvailabilityMock>();

builder.Services.AddScoped<Discount>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderRequest).Assembly));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
