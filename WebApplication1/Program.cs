using BLL;
using BLL.Iservices;
using BLL.Services;
using Dal_Repository.Irepository;
using Dal_Repository.models;
using Dal_Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<Irepository<Appointment>, AppointmentsRepository>();
builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();
builder.Services.AddAutoMapper(typeof(MyMapper));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

// Add localization middleware
app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
