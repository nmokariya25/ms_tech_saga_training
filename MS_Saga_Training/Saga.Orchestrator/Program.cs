var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure all the other Microservices
builder.Services.AddHttpClient("OrderService", c => c.BaseAddress = new Uri("http://localhost:5184"));
builder.Services.AddHttpClient("InventoryService", c => c.BaseAddress = new Uri("http://localhost:5084"));
builder.Services.AddHttpClient("NotificationService", c => c.BaseAddress = new Uri("http://localhost:5140"));


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
