using KafkaConsumer.DAL;
using KafkaConsumer.DAL.Repositories;
using KafkaConsumer.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//swager config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//swager config

//database config
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSingleton<AppDbContext>();
//database config


/* kafka service config*/
//builder.Services.AddSingleton<IHostedService, KafkaConsumerService>();
//builder.Services.AddHostedService<KafkaConsumerService>();
/* kafka service config*/


builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IFactoryRepository, FactoryRepository>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IProductionLineRepository, ProductionLineRepository>();
builder.Services.AddScoped<IStatusRecordRepository, StatusRecordRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.Cookie.HttpOnly = true;
           options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
           options.SlidingExpiration = true;
           options.LoginPath = "/account/login";
           options.AccessDeniedPath = "/account/accessdenied";
       });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kafka consumer API");
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
