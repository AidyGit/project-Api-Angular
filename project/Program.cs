using Microsoft.EntityFrameworkCore;
using project.Customer.Interfaces;
using project.Customer.Repositories;
using project.Customer.Repository;
using project.Customer.Services;
using project.Data;
using project.Manage.Interfaces;
using project.Manage.Repository;
using project.Manage.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-M9GE62O;DataBase=216259465_ChineseSale;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;"));


//builder.Services.AddDbContext<MarketDbContext>(options =>
//        options.UseSqlServer("ConnectionStrings"));


// Register services
builder.Services.AddScoped<IUserService, UserService>();

// Gift service
builder.Services.AddScoped<IGiftService, GiftService>();

//Gifts Repo
builder.Services.AddScoped<IGiftRepository, GiftRepository>();

//Register Repo
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Donors Service
builder.Services.AddScoped<IDonorsService, DonorService>();

// Donors Repo
builder.Services.AddScoped<IDonorsRepository, DonorsRepository>();

// Donation service
builder.Services.AddScoped<IDonationService, DonationService>();

//Donation Repo
builder.Services.AddScoped<IDonationRepository, DonationRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
