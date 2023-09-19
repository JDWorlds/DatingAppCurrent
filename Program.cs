using System.Text;
using datingApp.Data;
using datingApp.Extensions;
using datingApp.Interfaces;
using datingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Extension Services
builder.Services.AddApplicationServices(builder.Configuration);

// Authentication Setting
builder.Services.AddIdentityServices(builder.Configuration);

//-----------------------------------------------------------------//

var app = builder.Build();
// Using Cors Authentication
app.UseCors(builder => builder.AllowAnyHeader().WithOrigins("http://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
