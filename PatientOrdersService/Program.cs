using AutoMapper;
using PatientOrdersService.Data;
using PatientOrdersService.Mappings;
using PatientOrdersService.Middlewares;
using PatientOrdersService.Repositories;
using PatientOrdersService.Repositories.Interfaces;
using PatientOrdersService.Services;
using PatientOrdersService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// PostgreSQL Connection Factory
builder.Services.AddSingleton<IDbConnectionFactory>(sp =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    return new NpgsqlConnectionFactory(connStr);
});

// Repository
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionMiddleware>();
app.MapControllers();
app.Run();
