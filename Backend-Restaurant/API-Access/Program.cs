using Business;
using Dal;
using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, MockOrderRepository>();

var app = builder.Build();

app.UseCors(builder =>
{
    builder
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //var context = services.GetRequiredService<RestaurandDbContext>();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("AllowAll");

app.UseCors(builder =>
{
    builder
        .WithOrigins("http://localhost:3000", "https://localhost:3000") 
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
