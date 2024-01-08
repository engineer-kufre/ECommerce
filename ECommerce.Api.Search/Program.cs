using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using ECommerce.Api.Search.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var servicesSetting = builder.Configuration.GetSection("Services").Get<Services>();

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient("OrderService", config =>
{
    config.BaseAddress = new Uri(servicesSetting.Orders ?? "");
});
builder.Services.AddHttpClient("ProductService", config =>
{
    config.BaseAddress = new Uri(servicesSetting.Products ?? "");
});
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
