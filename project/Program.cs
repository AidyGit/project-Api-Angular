using Microsoft.EntityFrameworkCore;
using project.Customer.Interfaces;
using project.Customer.Repository;
using project.Customer.Services;
using project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=Srv2\\pupils;DataBase=216259465_ChineseSale;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;"));



//builder.Services.AddDbContext<MarketDbContext>(options =>
//        options.UseSqlServer("ConnectionStrings"));


// Register services
builder.Services.AddScoped<IUserService, UserService>();

//Register Repo

builder.Services.AddScoped<IUserRepository, UserRepository>();
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
