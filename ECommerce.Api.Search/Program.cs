using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using ECommerce.Api.Search.Settings;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var servicesSetting = builder.Configuration.GetSection("Services").Get<Services>();

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpClient("OrderService", config =>
{
    config.BaseAddress = new Uri(servicesSetting.Orders ?? "");
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("ProductService", config =>
{
    config.BaseAddress = new Uri(servicesSetting.Products ?? "");
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("CustomerService", config =>
{
    config.BaseAddress = new Uri(servicesSetting.Customers ?? "");
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
